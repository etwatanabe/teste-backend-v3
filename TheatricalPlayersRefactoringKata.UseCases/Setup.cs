using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Valhalla.Lib.SharedKernel;

public static class UseCasesServiceExtensions
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services, ILogger logger)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        logger.LogInformation("{Project} services registered", "UseCases");

        return services;
    }
}