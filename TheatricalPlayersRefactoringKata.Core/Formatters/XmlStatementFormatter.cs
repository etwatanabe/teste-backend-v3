using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Text;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        private readonly Dictionary<string, ICalculator> _calculators;

        public XmlStatementFormatter(Dictionary<string, ICalculator> calculators)
        {
            _calculators = calculators;
        }

        public string Format(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            decimal totalAmount = 0;
            int totalCredits = 0;
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
                var play = plays[perf.PlayID];
                var calculator = _calculators[play.Type.ToString()];
                decimal thisAmount = calculator.CalculateAmount(perf, play);
                int thisCredits = calculator.CalculateCredits(perf, play);

                var item = new XElement("Item",
                    new XElement("AmountOwed", thisAmount),
                    new XElement("EarnedCredits", thisCredits),
                    new XElement("Seats", perf.Audience));
                items.Add(item);

                totalAmount += thisAmount;
                totalCredits += thisCredits;
            }

            statement.Root.Add(new XElement("AmountOwed", totalAmount));
            statement.Root.Add(new XElement("EarnedCredits", totalCredits));

            using (var ms = new MemoryStream())
            {
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
        }
    }
}
