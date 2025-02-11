using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbActorsDto
    {
        public IEnumerable<Actor> Actors { get; set; } = null!;
    }
}
