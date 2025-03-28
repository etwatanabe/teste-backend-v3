using MediatR;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Events.Domain;

namespace TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Handlers;

internal class SummarizeInvoiceHandler : INotificationHandler<SummarizeInvoiceEvent>
{
    private readonly IEventBus _eventBus;

    public SummarizeInvoiceHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(SummarizeInvoiceEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(notification, cancellationToken);
    }
}
