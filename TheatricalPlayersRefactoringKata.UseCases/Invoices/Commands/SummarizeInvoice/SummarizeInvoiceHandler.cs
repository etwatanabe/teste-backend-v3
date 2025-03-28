using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Extensions;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.SummarizeInvoice;

internal class SummarizeInvoiceHandler : ICommandHandler<SummarizeInvoiceCommand, Result<InvoiceDTO>>
{
    private readonly IInvoiceService _invoiceService;

    public SummarizeInvoiceHandler(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    public async Task<Result<InvoiceDTO>> Handle(SummarizeInvoiceCommand request, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.SummarizeAsync(request.Customer, cancellationToken);
        if (!result.IsSuccess)
            return Result.Error(result.Errors.ToArray());

        return Result.Success(result.Value.ParseDTO());
    }
}