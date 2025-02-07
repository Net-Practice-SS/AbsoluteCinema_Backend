using AbsoluteCinema.Domain.Entities.Abstract;

namespace AbsoluteCinema.Domain.Entities
{
    public class TicketStatus : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
