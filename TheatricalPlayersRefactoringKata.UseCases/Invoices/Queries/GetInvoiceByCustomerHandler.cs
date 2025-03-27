using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Specifications;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Extensions;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Queries;

public class GetInvoiceByCustomerHandler : IQueryHandler<GetInvoiceByCustomerQuery, Result<InvoiceDTO>>
{
    private readonly IReadRepository<Invoice> _invoiceRepository;

    public GetInvoiceByCustomerHandler(IReadRepository<Invoice> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<InvoiceDTO>> Handle(GetInvoiceByCustomerQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.FirstOrDefaultAsync(new GetInvoiceByCustomerSpec(request.Customer), cancellationToken);
        if (invoice is null)
        {
            return Result.NotFound();
        }
        return Result<InvoiceDTO>.Success(invoice.ParseDTO());
    }
}
