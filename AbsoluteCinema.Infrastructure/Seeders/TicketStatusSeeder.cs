using Microsoft.EntityFrameworkCore;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.DbContexts;

namespace AbsoluteCinema.Infrastructure.Seeders;
public class TicketStatusSeeder
{
    public static async Task SeedTicketStatusesAsync(AppDbContext context)
    {
        // Если в таблице еще нет записей, то добавляем (простая проверка)
        if (!await context.TicketStatuses.AnyAsync())
        {
            var statuses = new List<TicketStatus>
            {
                new TicketStatus { Id = 1, Name = "Active" },
                new TicketStatus { Id = 2, Name = "Inactive" },
                new TicketStatus { Id = 3, Name = "Hold" },
                new TicketStatus { Id = 4, Name = "Returned" }
            };
                
            context.TicketStatuses.AddRange(statuses);
            await context.SaveChangesAsync();
        }
    }
}