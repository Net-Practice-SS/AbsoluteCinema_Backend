using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.EntitiesIdentity;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.BirthDate).IsRequired();

            // Relations with table Ticket
            builder.HasMany(u => u.Tickets)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
