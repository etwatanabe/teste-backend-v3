using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Queries;

public record GetInvoiceByCustomerQuery(string Customer) : IQuery<Result<InvoiceDTO>>;
