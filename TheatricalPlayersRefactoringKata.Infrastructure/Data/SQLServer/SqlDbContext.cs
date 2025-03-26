using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using System.Reflection;
using Valhalla.Lib.SharedKernel;
using TheatricalPlayersRefactoringKata.Core.Common.Exceptions;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.SQLServer;

public class SqlDbContext(IServiceProvider serviceProvider,
                          DbContextOptions<SqlDbContext> options)
    : DbContext(options)
{
    private readonly IDomainEventDispatcher? _domainEventDispatcher = serviceProvider?.GetRequiredService<IDomainEventDispatcher>();
    
    public DbSet<Play> Play => Set<Play>();
    public DbSet<Invoice> Invoice => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            // publish domain events
            if (_domainEventDispatcher is not null)
            {
                await _domainEventDispatcher.DispatchEvents(entitiesWithEvents);//.NoWait();
            }

            //// publish kafka integration events
            //if (_kafkaIntegrationEventStore is not null)
            //{
            //    var kafkaIntegrationEvents = entitiesWithEvents
            //      .SelectMany(e => e.DomainEvents.OfType<KafkaIntegrationEventBase>())
            //      .ToList();

            //    if (kafkaIntegrationEvents.Any())
            //        await _kafkaIntegrationEventStore.SaveEvents(kafkaIntegrationEvents);
            //}

            //if (_rabbitMQIntegrationEventStore is not null)
            //{
            //    var rabbitIntegrationEvents = entitiesWithEvents
            //       .SelectMany(e => e.DomainEvents.OfType<RabbitMQIntegrationEventBase>())
            //       .ToList();

            //    // publish rabbitmq integration events
            //    if (rabbitIntegrationEvents.Any())
            //        await _rabbitMQIntegrationEventStore.SaveEvents(rabbitIntegrationEvents, cancellationToken);
            //}

            return result;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new BusinessException("A record with these values already exists. Please check the data.");
        }
        catch (Exception ex)
        {
            throw new BusinessException("An unexpected error occurred while saving the entity.", ex);
        }
        finally
        {
            // ignore events if no dispatcher provided
            if (_domainEventDispatcher is not null)
            {
                // dispatch events only if save was successful
                var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
                    .Select(e => e.Entity)
                    .Where(e => e.DomainEvents.Any())
                    .ToArray();

                await _domainEventDispatcher.ClearEvents(entitiesWithEvents);
            }
        }
    }

    public override int SaveChanges() =>
          SaveChangesAsync().GetAwaiter().GetResult();
}
