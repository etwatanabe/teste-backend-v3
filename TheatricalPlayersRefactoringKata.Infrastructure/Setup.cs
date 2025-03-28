using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Data.SQLServer;
using TheatricalPlayersRefactoringKata.Infrastructure.EventBus;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatter;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Infrastructure;

public static class Setup
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ILogger logger)
    {
        services.AddDbContext<SqlDbContext>(options =>
        {
            options.UseSqlite("Data Source=TheatricalPlayersRefactoringKata.db");
        });

        services.AddSingleton<InMemoryEventBus>();
        services.AddSingleton<IEventBus>(sp => sp.GetRequiredService<InMemoryEventBus>());
        services.AddHostedService<LocalEventConsumer>();

        services.AddScoped<SqlDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddScoped<TextInvoiceFormatter>();
        services.AddScoped<XmlInvoiceFormatter>();
        services.AddScoped<IInvoiceFormatterFactory, InvoiceFormatterFactory>();

        services.AddMediatRDomainEventDispatcher(logger);

        logger.LogInformation("{Project} services registered", "Infrastructure");


        return services;
    }
}
