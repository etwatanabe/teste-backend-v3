using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatter;

public class XmlInvoiceFormatter : IInvoiceFormatter
{
    public string Format(Invoice invoice)
    {
        XDocument statement = new XDocument(
               new XDeclaration("1.0", "utf-8", null),
               new XElement("Statement",
                   new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                   new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                   new XElement("Customer", invoice.Customer),
                   new XElement("Items")
               )
           );

        XElement items = statement.Root.Element("Items");

        foreach (var perf in invoice.Performances)
        {
            var item = new XElement("Item",
                new XElement("AmountOwed", perf.AmountOwed),
                new XElement("EarnedCredits", perf.EarnedCredits),
                new XElement("Seats", perf.Audience));
            items.Add(item);
        }

        statement.Root.Add(new XElement("AmountOwed", invoice.TotalAmountOwed));
        statement.Root.Add(new XElement("EarnedCredits", invoice.TotalEarnedCredits));

        using (var ms = new MemoryStream())
        {
            // Essa pegadinha aqui é danada (BOMBOM)zinho
            byte[] bom = new byte[] { 0xEF, 0xBB, 0xBF, 0xEF, 0xBB, 0xBF };
            ms.Write(bom, 0, bom.Length);
            statement.Save(ms);
            ms.Flush();

            ms.Position = 0;

            using (var reader = new StreamReader(ms, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        //using (var ms = new MemoryStream())
        //{
        //    statement.Save(ms);
        //    ms.Flush();
        //    ms.Position = 0;
        //
        //    using (var reader = new StreamReader(ms, Encoding.UTF8))
        //    {
        //        return reader.ReadToEnd();
        //    }
        //}
    }
}
