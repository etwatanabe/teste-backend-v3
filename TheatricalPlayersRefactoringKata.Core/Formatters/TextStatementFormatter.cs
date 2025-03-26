using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Formatters;

public class TextStatementFormatter : IStatementFormatter
{
    private readonly Dictionary<string, ICalculator> _calculators;

    public TextStatementFormatter(Dictionary<string, ICalculator> calculators)
    {
        _calculators = calculators;
    }

    public string Format(Invoice invoice, Dictionary<Guid, Play> plays)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        decimal totalAmount = 0;
        int totalCredits = 0;
        string result = string.Format("Statement for {0}\n", invoice.Customer);

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayID];
            var calculator = _calculators[play.Type.ToString()];
            decimal thisAmount = calculator.CalculateAmount(perf, play);
            totalCredits += calculator.CalculateCredits(perf, play);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += string.Format("You earned {0} credits\n", totalCredits);
        return result;
    }
}