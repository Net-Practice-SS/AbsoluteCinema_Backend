using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class ActorStrategy : IEntityStrategy<Actor>
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly ICollection<int> _moviesIds;

        public ActorStrategy(
            string firstName = null!,
            string lastName = null!,
            ICollection<int> moviesIds = null!)
        {
            _firstName = firstName;
            _lastName = lastName;
            _moviesIds = moviesIds;
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
            if (_moviesIds != null && _moviesIds.Count > 0)
            {
                query = query.Where(
                    a => a.MovieActor.Any(
                        ma => _moviesIds.Contains(ma.MovieId)));
            }

            return query;
        }
    }
}
