using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.SQL.Config.AccountConfig;

public class PlayConfiguration : IEntityTypeConfiguration<Play>
{
    public void Configure(EntityTypeBuilder<Play> builder)
    {
        builder.ToTable(nameof(Play));
        builder.HasKey(a => a.Id);

        builder
            .HasIndex(m => m.Name)
            .IsUnique();
        builder
            .HasIndex(m => m.Type);

        builder
            .Property(ca => ca.Name)
            .IsRequired();
        builder
            .Property(ca => ca.Lines)
            .IsRequired();
        builder
            .Property(ca => ca.Type)
            .IsRequired();
    }
}
