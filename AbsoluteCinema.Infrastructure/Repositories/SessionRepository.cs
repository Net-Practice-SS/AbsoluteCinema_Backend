using Microsoft.EntityFrameworkCore;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Session>> GetFilteredSessions(IEntityStrategy<Session> strategy)
        {
            var query = GetWithStrategy(strategy);
            return await query.ToListAsync();
        }
    }
}
