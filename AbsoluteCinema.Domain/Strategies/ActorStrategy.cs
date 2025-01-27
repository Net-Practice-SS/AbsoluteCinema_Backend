using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class ActorStrategy : IEntityStrategy<Actor>
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly ICollection<Movie> _movies;

        public ActorStrategy(
            string firstName = null!,
            string lastName = null!,
            ICollection<Movie> movies = null!)
        {
            _firstName = firstName;
            _lastName = lastName;
            _movies = movies;
        }

        public IQueryable<Actor> ApplyFilter(IQueryable<Actor> query)
        {
            if (!string.IsNullOrWhiteSpace(_firstName))
            {
                query = query.Where(a => a.FirstName.Contains(_firstName));
            }
            if (!string.IsNullOrWhiteSpace(_lastName))
            {
                query = query.Where(a => a.LastName.Contains(_lastName));
            }
            if (_movies != null && _movies.Count > 0)
            {
                query = query.Where(
                    a => a.MovieActor != null && a.MovieActor.Any(
                        ma => _movies.Any(
                            m => m.Id == ma.MovieId)));
            }

            return query;
        }
    }
}
