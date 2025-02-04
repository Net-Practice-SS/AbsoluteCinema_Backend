using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<IEnumerable<Session>> GetFilteredSessions(IEntityStrategy<Session> strategy);
    }
}
