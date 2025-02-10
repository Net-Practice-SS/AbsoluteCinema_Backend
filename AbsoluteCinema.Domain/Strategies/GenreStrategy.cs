using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class GenreStrategy : IEntityStrategy<Genre>
    {
        private readonly string _title;
        private readonly ICollection<int> _moviesIds;

        public GenreStrategy(
            string title = null!,
            ICollection<int> moviesIds = null!)
        {
            _title = title;
            _moviesIds = moviesIds;
        }

        public IQueryable<Genre> ApplyFilter(IQueryable<Genre> query)
        {
            if (!string.IsNullOrWhiteSpace(_title))
            {
                query = query.Where(g => g.Title.Contains(_title));
            }
            if (_moviesIds != null && _moviesIds.Count > 0)
            {
                query = query.Where(
                    g => g.MovieGenre.Any(
                        mg => _moviesIds.Contains(mg.MovieId)));
            }

            return query;
        }
    }
}
