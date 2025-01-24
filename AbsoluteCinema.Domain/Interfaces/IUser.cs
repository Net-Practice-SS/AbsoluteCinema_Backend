using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IUser
    {
        string Id { get; set; }
        ICollection<Ticket> Tickets { get; set; }
    }
}
