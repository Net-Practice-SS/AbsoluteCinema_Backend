namespace AbsoluteCinema.Domain.Entities
{
    public class TicketStatus
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
