using Microsoft.AspNetCore.Identity;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Infrastructure.Identity.Data
{
    public class ApplicationUser : IdentityUser<int>, IUser, IEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = null!;
    }
}
