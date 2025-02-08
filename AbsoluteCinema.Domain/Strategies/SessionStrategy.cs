using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class SessionStrategy : IEntityStrategy<Session>
    {
        private readonly int? _movieId;
        private readonly DateTime? _date;
        private readonly int? _hallId;

        public SessionStrategy(
            int? movieId = null!,
            DateTime? date = null!,
            int? hallId = null!)
        {
            _movieId = movieId;
            _date = date;
            _hallId = hallId;
        }

        public IQueryable<Session> ApplyFilter(IQueryable<Session> query)
        {
            if (_movieId.HasValue)
            {
                query = query.Where(s => s.MovieId == _movieId.Value);
            }
            if (_date.HasValue)
            {
                query = query.Where(s => s.Date.Date == _date.Value.Date);
            }
            if (_hallId.HasValue)
            {
                query = query.Where(s => s.HallId == _hallId.Value);
            }

            return query;
        }
    }
}
