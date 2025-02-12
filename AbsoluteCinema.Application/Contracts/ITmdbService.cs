using AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO;

namespace AbsoluteCinema.Application.Contracts
{
    public interface ITmdbService
    {
        public Task<IEnumerable<TmdbGenreDto>> GetGenresAsync();
        public Task<IEnumerable<TmdbMovieDto>> GetMoviesAsync(int page = 1);
        public Task<IEnumerable<TmdbCastDto>> GetActorsAsync(int movieId);
        public Task<string> GetMovieTrailerAsync(int movieId);
    }
}
