﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Infrastructure.Identity.Data;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.BirthDate).IsRequired();

            /*// Relations with table Ticket
            builder.HasMany(u => u.Tickets)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
