using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.wpf.DataAccess.Configurations;

//according to teacher's demo-video (058), moved configuration for TitlarPerFörFattare from OnModelCreating() into new class TitlarPerFörfattareEntityTypeConfiguration.cs:
public class TitlarPerFörfattareEntityTypeConfiguration : IEntityTypeConfiguration<TitlarPerFörfattare>
{
    public void Configure(EntityTypeBuilder<TitlarPerFörfattare> builder)
    {
            builder
                .HasNoKey()
                .ToView("TitlarPerFörfattare");

            builder.Property(e => e.Lagervärde)
                .HasMaxLength(44)
                .IsUnicode(false);
            builder.Property(e => e.Namn).HasMaxLength(41);
            builder.Property(e => e.Titlar)
                .HasMaxLength(15)
                .IsUnicode(false);
            builder.Property(e => e.Ålder)
                .HasMaxLength(15)
                .IsUnicode(false);
    }
}