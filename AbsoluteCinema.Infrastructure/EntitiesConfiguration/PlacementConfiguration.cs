using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class PlacementConfiguration : IEntityTypeConfiguration<Placement>
    {
        public void Configure(EntityTypeBuilder<Placement> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Place).IsRequired();
            builder.Property(p => p.Row).IsRequired();
            builder.Property(p => p.Price).IsRequired();
        }
    }
}
