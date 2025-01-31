using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class GenreStrategy : IEntityStrategy<Genre>
    {
        private readonly string _title;
        private readonly ICollection<Movie> _movies;

        public GenreStrategy(
            string title = null!,
            ICollection<Movie> movies = null!)
        {
            _title = title;
            _movies = movies;
        }

        public IQueryable<Genre> ApplyFilter(IQueryable<Genre> query)
        {
            if (!string.IsNullOrWhiteSpace(_title))
            {
                query = query.Where(g => g.Title.Contains(_title));
            }
            if (_movies != null && _movies.Count > 0)
            {
                query = query.Where(
                    g => g.MovieGenre.Any(
                        mg => _movies.Any(
                            m => m.Id == mg.MovieId)));
            }

            return query;
        }
    }
}
