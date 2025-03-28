using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using Valhalla.Lib.Result;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IInvoiceService
{
    Task<Result<Invoice>> AddPerformanceAsync(string customer, Performance performance, CancellationToken cancellationToken);
    decimal CalculateAmount(Play play, Performance performance);
    int CalculateCredits(Play play, Performance performance);
    Task<Result<Invoice>> SummarizeAsync(string customer, CancellationToken cancellationToken);
}