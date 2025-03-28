using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.SQL.Config.AccountConfig;

public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder.ToTable(nameof(Performance));

        builder.Property<Guid>("Id");
        builder.HasKey("Id");

        builder
            .Property(ca => ca.PlayName)
            .IsRequired();

        builder
            .Property(ca => ca.Audience)
            .IsRequired();

        builder
            .Property(ca => ca.AmountOwed)
            .IsRequired();

        builder
            .Property(ca => ca.EarnedCredits)
            .IsRequired();
    }
}
