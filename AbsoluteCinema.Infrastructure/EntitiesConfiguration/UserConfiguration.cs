using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            //builder.HasKey(u => new { u.Email, u.Password });
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
            builder.Property(u => u.BirthDate).IsRequired();

            // Relations with table Role
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            // Relations with table Ticket
            builder.HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}
