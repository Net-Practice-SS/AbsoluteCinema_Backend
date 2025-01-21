namespace AbsoluteCinema.Domain.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<MovieActor> MovieActor { get; set; }
    }
}
