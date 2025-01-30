using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Enums;
using AbsoluteCinema.Domain.Interfaces;

namespace AbsoluteCinema.Domain.Strategies
{
    public class MovieStrategy : IEntityStrategy<Movie>
    {
        private readonly string _title;
        private readonly string _discription;
        private readonly double? _score;
        private readonly bool? _adult;
        private readonly MovieLanguageEnum? _language;
        private readonly DateTime? _releaseYearFrom;
        private readonly DateTime? _releaseYearTo;
        private readonly ICollection<Actor> _actors;
        private readonly ICollection<Genre> _genres;

        public MovieStrategy(
            string title = null!, 
            string description = null!, 
            double? score = null!, 
            bool? adult = null!, 
            MovieLanguageEnum? language = null!, 
            DateTime? releaseYearFrom = null!,
            DateTime? releaseYearTo = null!,
            ICollection<Actor> actors = null!,
            ICollection<Genre> genres = null!)
        {
            _title = title;
            _discription = description;
            _score = score;
            _adult = adult;
            _language = language;
            _releaseYearFrom = releaseYearFrom;
            _releaseYearTo = releaseYearTo;
            _actors = actors;
            _genres = genres;
        }

        public IQueryable<Movie> ApplyFilter(IQueryable<Movie> query)
        {
            if (!string.IsNullOrWhiteSpace(_title))
            {
                query = query.Where(m => m.Title.Contains(_title));
            }
            if (!string.IsNullOrWhiteSpace(_discription))
            {
                query = query.Where(m => !string.IsNullOrWhiteSpace(m.Discription) && m.Discription.Contains(_discription));
            }
            if (_score.HasValue)
            {
                query = query.Where(m => m.Score >= _score.Value);
            }
            if (_adult.HasValue)
            {
                query = query.Where(m => m.Adult == _adult.Value);
            }
            if (_language.HasValue)
            {
                query = query.Where(m => m.Language == _language.Value);
            }
            if (_releaseYearFrom.HasValue && _releaseYearTo.HasValue)
            {
                query = query.Where(m => m.ReleaseDate.HasValue &&
                             m.ReleaseDate.Value.Year >= _releaseYearFrom.Value.Year &&
                             m.ReleaseDate.Value.Year <= _releaseYearTo.Value.Year);
            }
            if (_actors != null && _actors.Count > 0)
            {
                query = query.Where(
                    m => m.MovieActor.Any(
                        ma => _actors.Any(
                            a => a.Id == ma.ActorId)));
            }
            if (_genres != null && _genres.Count > 0)
            {
                query = query.Where(
                    m => m.MovieGenre.Any(
                        mg => _genres.Any(
                            g => g.Id == mg.GenreId)));
            }

            return query;
        }
    }
}
