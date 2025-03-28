using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Events.Domain;

public sealed class SummarizeInvoiceEvent(Invoice invoice) : DomainEventBase
{
    public Invoice Invoice { get; init; } = invoice;
}
