using AbsoluteCinema.Domain.Entities.Abstract;

namespace AbsoluteCinema.Domain.Entities
{
    public class Session : BaseEntity
    {
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }

        public Movie Movie { get; set; } = null!;
        public Hall Hall { get; set; } = null!;
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
