using AbsoluteCinema.Application.DTO.Entities;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task<MovieDto?> GetMovieByIdAsync(int id);
        Task DeleteMovieAsync(int id);
        Task UpdateMovieAsync(MovieDto movieDto);
        Task<int> CreateMovieAsync(MovieDto movieDto);
    }
}
