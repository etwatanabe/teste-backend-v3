using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IEventBus
{
    ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken ct) where TEvent : DomainEventBase;
}
