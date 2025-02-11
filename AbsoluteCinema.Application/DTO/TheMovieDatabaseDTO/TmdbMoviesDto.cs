using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbMoviesDto
    {
        public IEnumerable<Movie> Movies { get; set; } = null!;
    }
}
