using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Application.Contracts
{
    public interface ITmdbService
    {
        public Task<IEnumerable<Genre>> GetGenresAsync();
        public Task<IEnumerable<Movie>> GetMoviesAsync(int page = 1);
        public Task<IEnumerable<Actor>> GetActorsAsync(int movieId);
    }
}
