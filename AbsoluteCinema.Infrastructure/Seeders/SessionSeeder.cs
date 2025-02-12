using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.DbContexts;


namespace AbsoluteCinema.Infrastructure.Seeders
{
    public class SessionSeeder
    {
        public static async Task SeedSessionsAsync(AppDbContext context)
        {
            if (!context.Sessions.Any())
            {
                var random = new Random();
                var randomHall = random.Next(1, context.Halls.Count() + 1);
                var movies = context.Movies.ToList();
                foreach (var movie in movies)
                {
                    context.Sessions.Add(new Session() 
                    { 
                        MovieId = movie.Id,
                        HallId = context.Halls.Find(randomHall)!.Id,
                        Date = (DateTime.Now).AddDays(random.Next(1, 7)),
                    });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
