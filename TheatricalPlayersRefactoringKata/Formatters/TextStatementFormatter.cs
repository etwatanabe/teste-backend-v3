using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata.Formatters;

public class TextStatementFormatter : IStatementFormatter
{
    private readonly Dictionary<string, ICalculator> _calculators;

    public TextStatementFormatter(Dictionary<string, ICalculator> calculators)
    {
        _calculators = calculators;
    }

    public string Format(Invoice invoice, Dictionary<string, Play> plays)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        decimal totalAmount = 0;
        int totalCredits = 0;
        string result = string.Format("Statement for {0}\n", invoice.Customer);

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var calculator = _calculators[play.Type];
            decimal thisAmount = calculator.CalculateAmount(perf, play);
            totalCredits += calculator.CalculateCredits(perf, play);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", totalCredits);
        return result;
    }
}