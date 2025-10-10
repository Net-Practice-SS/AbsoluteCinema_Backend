using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Infrastructure.Seeders
{
    public class SessionSeeder
    {
        public static async Task SeedSessionsAsync(AppDbContext context)
        {
            if (await context.Sessions.AnyAsync())
                return;

            var random = new Random();
            var movies = await context.Movies.ToListAsync();
            var halls = await context.Halls.ToListAsync();

            const int sessionsPerMovie = 4;

            foreach (var movie in movies)
            {
                for (int i = 0; i < sessionsPerMovie; i++)
                {
                    var hall = halls[random.Next(halls.Count)];
                    var date = DateTime.Now.AddDays(random.Next(1, 7))
                                            .AddHours(random.Next(10, 22)); 

                    bool exists = await context.Sessions.AnyAsync(s =>
                        s.MovieId == movie.Id &&
                        s.HallId == hall.Id &&
                        s.Date.Date == date.Date);

                    if (!exists)
                    {
                        context.Sessions.Add(new Session
                        {
                            MovieId = movie.Id,
                            HallId = hall.Id,
                            Date = date
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
