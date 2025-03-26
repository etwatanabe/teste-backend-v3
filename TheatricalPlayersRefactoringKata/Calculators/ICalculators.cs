using System;

namespace TheatricalPlayersRefactoringKata.Calculators;

public interface ICalculator
{
    decimal CalculateAmount(Performance performance, Play play);
    int CalculateCredits(Performance performance, Play play);
}
