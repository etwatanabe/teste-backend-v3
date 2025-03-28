using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.CreateInvoice;

public record CreateInvoiceCommand(string Customer) : ICommand<Result<InvoiceDTO>>;
