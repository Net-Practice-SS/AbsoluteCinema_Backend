namespace AbsoluteCinema.Application.DTO.UsersDTO;

public class UpdateUserDto
{
    public string Id { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public string? PhoneNumber { get; set; } = null!;
}