using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
    {
        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {
            builder.HasKey(ts => ts.Id);
            builder.Property(ts => ts.Name).IsRequired();

            // Relations with table Ticket
            builder.HasMany(ts => ts.Tickets)
                .WithOne(t => t.Status)
                .HasForeignKey(t => t.StatusId);
        }
    }
}
