using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.SQL.Config.AccountConfig;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable(nameof(Invoice));
        builder.HasKey(a => a.Id);

        builder
            .HasIndex(m => m.Customer);

        builder
          .Property(ca => ca.TotalAmountOwed)
          .IsRequired();

        builder
            .Property(ca => ca.TotalEarnedCredits)
            .IsRequired();

        builder.HasMany(c => c.Performances)
          .WithOne()
          .HasForeignKey(nameof(Performance) + "Id")
          .OnDelete(DeleteBehavior.Cascade);
    }
}
