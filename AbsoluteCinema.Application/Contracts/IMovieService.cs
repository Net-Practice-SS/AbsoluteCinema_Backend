using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.MoviesDTO;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync(GetAllMoviesDto getAllMoviesDto);
        Task<MovieDto?> GetMovieByIdAsync(int id);
        Task DeleteMovieAsync(int id);
        Task UpdateMovieAsync(UpdateMovieDto updateMovieDto);
        Task<int> CreateMovieAsync(CreateMovieDto movieDto);
        Task<IEnumerable<MovieDto>> GetMovieWithStrategyAsync(GetMovieWithStrategyDto getMovieWithStrategyDto);
    }
}
