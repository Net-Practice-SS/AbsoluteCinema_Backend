using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbGenresDto
    {
        public IEnumerable<Genre> Genres { get; set; } = null!;
    }
}
