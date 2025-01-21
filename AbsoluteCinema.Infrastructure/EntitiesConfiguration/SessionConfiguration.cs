using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.EntitiesConfiguration
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder) 
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Date);

            // Relations with table Movie
            builder.HasOne(s => s.Movie)
                .WithMany(m => m.Sessions)
                .HasForeignKey(s => s.MovieId)
                .IsRequired();

            // Relations with table Ticket
            builder.HasMany(s => s.Tickets)
                .WithOne(t => t.Session)
                .HasForeignKey(t => t.SessionId);
        }
    }
}
