using AbsoluteCinema.Domain.Entities.Abstract;

namespace AbsoluteCinema.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Title { get; set; } = null!;

        public ICollection<MovieGenre> MovieGenre { get; set; } = new List<MovieGenre>();
    }
}
