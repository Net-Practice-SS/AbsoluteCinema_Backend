using AbsoluteCinema.Domain.Entities.Abstract;

namespace AbsoluteCinema.Domain.Entities
{
    public class Actor : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<MovieActor> MovieActor { get; set; } = new List<MovieActor>();
    }
}
