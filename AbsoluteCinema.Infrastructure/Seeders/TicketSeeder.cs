using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Infrastructure.Seeders
{
    public class TicketSeeder
    {
        public static async Task SeedTicketsAsync(AppDbContext context)
        {
            // Если уже есть билеты — выходим
            if (await context.Tickets.AnyAsync())
                return;

            var random = new Random();

            // Загружаем нужные данные
            var sessions = await context.Sessions.ToListAsync();
            var statuses = await context.TicketStatuses.ToListAsync();
            var users = await context.Users.ToListAsync();

            // Проверка, что данные есть
            if (!sessions.Any() || !statuses.Any() || !users.Any())
                return;

            var tickets = new List<Ticket>();

            foreach (var session in sessions)
            {
                // Допустим, на каждый сеанс 5 случайных билетов
                int ticketCount = random.Next(3, 7);

                for (int i = 0; i < ticketCount; i++)
                {
                    var user = users[random.Next(users.Count)];
                    var status = statuses[random.Next(statuses.Count)];

                    tickets.Add(new Ticket
                    {
                        SessionId = session.Id,
                        UserId = user.Id,
                        Row = random.Next(1, 10),
                        Place = random.Next(1, 15),
                        StatusId = status.Id,
                        Price = Math.Round(random.NextDouble() * 200 + 50, 2)
                    });
                }
            }

            await context.Tickets.AddRangeAsync(tickets);
            await context.SaveChangesAsync();
        }
    }
}
