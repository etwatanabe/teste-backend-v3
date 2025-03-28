using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Core;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Events.Domain;
using TheatricalPlayersRefactoringKata.UseCases;
using Valhalla.Lib.SharedKernel;

public static class Setup
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services, ILogger logger)
    {
        services.AddCoreServices(logger);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        services.AddScoped<IEventHandler<SummarizeInvoiceEvent>, SummarizeProcessorHandler>();

        logger.LogInformation("{Project} services registered", "UseCases");

        return services;
    }
}