using System.Globalization;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatter;

public class TextInvoiceFormatter : IInvoiceFormatter
{
    public string Format(Invoice invoice)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = string.Format("Statement for {0}\n", invoice.Customer);

        foreach (var perf in invoice.Performances)
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", perf.PlayName, perf.AmountOwed, perf.Audience);

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmountOwed);
        result += String.Format("You earned {0} credits\n", invoice.TotalEarnedCredits);

        return result;
    }
}
