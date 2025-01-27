using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Name).IsRequired().HasMaxLength(256);
            builder.HasIndex(h => h.Name).IsUnique();
            builder.HasIndex(h => h.Name).IsUnique();
            builder.Property(h => h.RowCount).IsRequired();
            builder.HasIndex(h => h.RowCount).IsUnique();
            builder.Property(h => h.PlaceCount).IsRequired();
            builder.HasIndex(h => h.PlaceCount).IsUnique();

            // Relations with table Session
            builder.HasMany(h => h.Sessions)
                .WithOne(s => s.Hall)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
