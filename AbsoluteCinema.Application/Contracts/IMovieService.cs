using AbsoluteCinema.Application.DTO.Entities;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMovies();
        Task<MovieDto?> GetMovieById(int id);
        Task DeleteMovie(int id);
        Task UpdateMovie(MovieDto movie);
        Task<int> CreateMovie(MovieDto movie);
    }
}
