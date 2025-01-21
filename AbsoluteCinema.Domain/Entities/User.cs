namespace AbsoluteCinema.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime BirthDate { get; set; }

        public Role Role { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
