using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(32);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(32);
            builder.HasIndex(a => new { a.FirstName, a.LastName }).IsUnique();

            // Relations with table MovieActor
            builder.HasMany(m => m.MovieActor)
                .WithOne(ma => ma.Actor)
                .HasForeignKey(ma => ma.ActorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
