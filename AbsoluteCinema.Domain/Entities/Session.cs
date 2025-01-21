namespace AbsoluteCinema.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }

        public Movie Movie { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
