namespace AbsoluteCinema.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int Row {  get; set; }
        public int Place {  get; set; }
        public int PlacementId { get; set; }
        public int StatusId { get; set; }

        public TicketStatus Status { get; set; }
        public Session Session { get; set; }
        public Interfaces.IUser User { get; set; }
    }
}
