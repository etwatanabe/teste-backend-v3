using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.SummarizeInvoice;

public record SummarizeInvoiceCommand(string Customer) : ICommand<Result<InvoiceDTO>>;
