using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        private readonly Dictionary<string, ICalculator> _calculators;

        public XmlStatementFormatter(Dictionary<string, ICalculator> calculators)
        {
            _calculators = calculators;
        }

        public string Format(Invoice invoice, Dictionary<string, Play> plays)
        {
            decimal totalAmount = 0;
            int totalCredits = 0;
            XDocument statement = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
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
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];
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

            return statement.ToString();
        }
    }
}
