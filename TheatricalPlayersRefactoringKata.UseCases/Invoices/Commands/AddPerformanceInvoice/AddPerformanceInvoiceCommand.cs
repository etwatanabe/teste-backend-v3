using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.AddPerformanceInvoice;

public record AddPerformanceInvoiceCommand(string Customer, Performance Performance) : ICommand<Result<InvoiceDTO>>;
