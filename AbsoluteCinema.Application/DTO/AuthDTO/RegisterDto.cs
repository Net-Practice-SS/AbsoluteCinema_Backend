namespace AbsoluteCinema.Application.DTO.AuthDTO;

public class RegisterDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}