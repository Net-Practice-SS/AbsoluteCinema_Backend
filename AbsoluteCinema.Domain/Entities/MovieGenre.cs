using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Entities
{
    public class MovieGenre : IEntity
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public Movie Movie { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
