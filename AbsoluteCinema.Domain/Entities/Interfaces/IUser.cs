namespace AbsoluteCinema.Domain.Entities.Interfaces
{
    public interface IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        ICollection<Ticket> Tickets { get; set; }
    }
}
