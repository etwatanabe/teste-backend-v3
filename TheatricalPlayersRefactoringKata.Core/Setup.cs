using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Core;

public static class Setup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, ILogger logger)
    {
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        services.AddScoped<IInvoiceService, InvoiceService>();

        logger.LogInformation("{Project} services registered", "Core");

        return services;
    }
}
