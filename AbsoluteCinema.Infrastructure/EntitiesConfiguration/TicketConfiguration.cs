using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Row);
            builder.Property(t => t.Place);

            // Relations with table Session
            builder.HasOne(t => t.Session)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SessionId)
                .IsRequired();

            /*// Relations with table User
            builder.HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .IsRequired();*/

            // Relations with table TicketStatus
            builder.HasOne(t => t.Status)
                .WithMany(ts => ts.Tickets)
                .HasForeignKey(t => t.StatusId)
                .IsRequired();
        }
    }
}
