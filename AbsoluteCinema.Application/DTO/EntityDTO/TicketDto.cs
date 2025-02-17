using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;
using AbsoluteCinema.Application.DTO.UsersDTO;

namespace AbsoluteCinema.Application.DTO.Entities;

public class TicketDto
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public int Row { get; set; }
    public int Place { get; set; }
    public double Price  { get; set; }
    public int StatusId { get; set; }
    
    public SessionDto Session { get; set; } = null!;
    public UserDto ApplicationUser { get; set; } = null!;
    public TicketStatusDto Status { get; set; } = null!;
}