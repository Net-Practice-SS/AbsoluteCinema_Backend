using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Infrastructure.Seeders
{
    public class HallSeeder
    {
        public static async Task SeedHallsAsync(AppDbContext context)
        {
            if (!context.Halls.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    context.Halls.Add(new Hall() { Name = $"№{i + 1}", RowCount = 4 + i, PlaceCount = 5 + i });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
