using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Extensions;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Extensions;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands;

internal class CreateInvoiceHandler : ICommandHandler<CreateInvoiceCommand, Result<InvoiceDTO>>
{
    private readonly IRepository<Invoice> _invoiceRepository;

    public CreateInvoiceHandler(IRepository<Invoice> invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<InvoiceDTO>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = new Invoice(request.Customer);
        var newInvoice = await _invoiceRepository.AddAsync(invoice, cancellationToken);

        await _invoiceRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newInvoice.ParseDTO());
    }
}
