using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Entities
{
    public class MovieActor : IEntity
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string CharacterName { get; set; } = null!;

        public Actor Actor { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
}
