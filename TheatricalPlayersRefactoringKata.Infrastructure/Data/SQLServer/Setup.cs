using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Infrastructure.Data.SQLServer;
using Valhalla.Lib.GuardClauses;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.SQL;

public static class Setup
{
    public static IServiceCollection AddSqlDbContext(this IServiceCollection services, ILogger logger)
    {
        services.AddDbContext<SqlDbContext>(options =>
        {
            options.UseSqlite(Guard.Against.NullOrWhiteSpace(Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING")));
        });
        services.AddTransient<SqlDbContext>();
        services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddMediatRDomainEventDispatcher(logger);


        return services;
    }
}
