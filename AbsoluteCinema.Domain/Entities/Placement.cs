namespace AbsoluteCinema.Domain.Entities
{
    public class Placement
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public int Row { get; set; }
        public double Price { get; set; }

        // In our ERD we have relation 1:1 between 'Placement' and 'Ticket', but 1:N is better here
        public ICollection<Ticket> Tickets { get; set; }
    }
}
