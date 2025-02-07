using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.GenresDTO;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync(GetAllGenreDto getAllGenreDto);
        Task<GenreDto?> GetGenreByIdAsync(int id);
        Task DeleteGenreAsync(int id);
        Task UpdateGenreAsync(UpdateGenreDto updateGenreDto);
        Task<int> CreateGenreAsync(CreateGenreDto createGenreDto);
        Task<IEnumerable<GenreDto>> GetGenreWithStrategyAsync(GetGenreWithStrategyDto getGenreWithStrategyDto);
        Task AddMovieToGenreAsync(MovieGenreDto movieGenreDto);
        Task DeleteMovieFromGenreAsync(MovieGenreDto movieGenreDto);
    }
}
