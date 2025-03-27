using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Extensions;

public static class InvoiceExtensions
{
    public static InvoiceDTO ParseDTO(this Invoice invoice)
        => new(invoice.Customer, invoice.Performances);
}