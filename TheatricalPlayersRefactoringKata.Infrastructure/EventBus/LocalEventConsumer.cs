using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Infrastructure.EventBus;
public class LocalEventConsumer : BackgroundService
{
    private readonly ChannelReader<DomainEventBase> _reader;
    private readonly IServiceProvider _provider;

    public LocalEventConsumer(InMemoryEventBus bus, IServiceProvider provider)
    {
        _reader = bus.Reader;
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var evt in _reader.ReadAllAsync(stoppingToken))
        {
            using var scope = _provider.CreateScope();

            var handlerType = typeof(IEventHandler<>).MakeGenericType(evt.GetType());
            var handler = scope.ServiceProvider.GetService(handlerType);

            if (handler is not null)
            {
                var method = handlerType.GetMethod("HandleAsync")!;
                await (Task)method.Invoke(handler, new object[] { evt, stoppingToken })!;
            }
        }
    }
}
