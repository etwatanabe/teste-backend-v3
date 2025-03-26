using System;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public class ComedyCalculator : ICalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        // Base amount
        int lines = play.Lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        decimal thisAmount = (decimal) lines / 10;
        
        // Specific amount
        thisAmount += 3 * performance.Audience;
        if (performance.Audience > 20)
        {
            thisAmount += 100 + 5 * (performance.Audience - 20);
        }
        return thisAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return (performance.Audience > 30 ? performance.Audience - 30 : 0) + performance.Audience / 5;
    }
}
