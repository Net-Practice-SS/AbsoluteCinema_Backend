using AbsoluteCinema.Domain.Entities.Abstract;
using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int Row {  get; set; }
        public int Place {  get; set; }
        public int StatusId { get; set; }
        public double Price { get; set; }

        public TicketStatus Status { get; set; } = null!;
        public Session Session { get; set; } = null!;
        public IUser ApplicationUser { get; set; } = null!;
    }
}
