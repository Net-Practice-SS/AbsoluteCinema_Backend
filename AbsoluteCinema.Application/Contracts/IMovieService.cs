using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using MovieActorDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieActorDto;
using MovieGenreDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieGenreDto;

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
        Task AddGenreToMovieAsync(MovieGenreDto movieGenreDto);
        Task DeleteGenreFromMovieAsync(MovieGenreDto movieGenreDto);
        Task AddActorToMovieAsync(MovieActorDto movieActorDto);
        Task DeleteActorFromMovieAsync(MovieActorDto movieActorDto);
        Task<IEnumerable<MovieDto>> GetAllMoviesWithIncludeAsync();
        Task<IEnumerable<MovieDto>> GetPersonalizedMovieSuggestionsAsync(int userId);
    }
}
