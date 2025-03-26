using System;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Calculators;

public interface ICalculator
{
    decimal CalculateAmount(Performance performance, Play play);
    int CalculateCredits(Performance performance, Play play);
}
