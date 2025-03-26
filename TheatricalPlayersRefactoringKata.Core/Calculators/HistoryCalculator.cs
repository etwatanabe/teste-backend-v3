using System;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public class HistoryCalculator : ICalculator
{
    private readonly TragedyCalculator _tragedyCalculator;
    private readonly ComedyCalculator _comedyCalculator;

    public HistoryCalculator()
        {
            _tragedyCalculator = new TragedyCalculator();
            _comedyCalculator = new ComedyCalculator();
        }

    public decimal CalculateAmount(Performance performance, Play play)
    {
        decimal tragedyAmount = _tragedyCalculator.CalculateAmount(performance, play);
        decimal comedyAmount = _comedyCalculator.CalculateAmount(performance, play);
        decimal thisAmount = tragedyAmount + comedyAmount;

        return thisAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return performance.Audience > 30 ? performance.Audience - 30 : 0;
    }
}
