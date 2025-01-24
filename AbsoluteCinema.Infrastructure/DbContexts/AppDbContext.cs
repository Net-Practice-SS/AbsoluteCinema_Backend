using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbsoluteCinema.Domain.Entities;
<<<<<<< Updated upstream
=======
using AbsoluteCinema.Infrastructure.EntitiesConfiguration;
using AbsoluteCinema.Infrastructure.EntitiesIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
>>>>>>> Stashed changes

namespace AbsoluteCinema.Infrastructure.DbContexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
<<<<<<< Updated upstream

=======
        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("DefaultConnection");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
>>>>>>> Stashed changes
    }
}
