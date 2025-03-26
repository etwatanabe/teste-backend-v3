using System;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public class TragedyCalculator : ICalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        // Base amount
        int lines = play.Lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        decimal thisAmount = (decimal) lines / 10;
        
        // Specific amount
        if (performance.Audience > 30)
        {
            thisAmount += 10 * (performance.Audience - 30);
        }
        return thisAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return performance.Audience > 30 ? performance.Audience - 30 : 0;
    }

}