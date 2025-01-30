using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class SessionStrategy : IEntityStrategy<Session>
    {
        private readonly int? _movieId;
        private readonly DateTime? _dateFrom;
        private readonly DateTime? _dateTo;
        private readonly int? _hallId;

        public SessionStrategy(
            int? movieId = null!,
            DateTime? dateFrom = null!,
            DateTime? dateTo = null!,
            int? hallId = null!)
        {
            _movieId = movieId;
            _dateFrom = dateFrom;
            _dateTo = dateTo;
            _hallId = hallId;
        }

        public IQueryable<Session> ApplyFilter(IQueryable<Session> query)
        {
            if (_movieId.HasValue)
            {
                query = query.Where(s => s.MovieId == _movieId.Value);
            }
            if (_dateFrom.HasValue)
            {
                query = query.Where(s => s.Date >= _dateFrom.Value.Date);
            }
            else
            {
                query = query.Where(s => s.Date >= DateTime.Now.Date);
            }
            if (_dateTo.HasValue)
            {
                query = query.Where(s => s.Date <= _dateTo.Value.Date);
            }
            /*if (_dateFrom.HasValue && _dateTo.HasValue)
            {
                query = query.Where(s => s.Date >= _dateFrom.Value.Date &&
                                    s.Date <= _dateTo.Value.Date);
            }*/
            if (_hallId.HasValue)
            {
                query = query.Where(s => s.HallId == _hallId.Value);
            }

            return query;
        }
    }
}
