using AbsoluteCinema.Application.DTO.Entities;

namespace AbsoluteCinema.Application.Contracts
{
    public interface ITmdbService
    {
        public Task<IEnumerable<GenreDto>> GetGenresAsync();
        public Task<IEnumerable<MovieDto>> GetMoviesAsync(int page = 1);
        public Task<IEnumerable<ActorDto>> GetActorsAsync(int movieId);
    }
}
