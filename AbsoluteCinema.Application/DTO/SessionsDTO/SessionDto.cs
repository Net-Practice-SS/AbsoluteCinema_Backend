namespace AbsoluteCinema.Application.DTO.SessionsDTO;

public class SessionDto
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public DateTime Date { get; set; }
    public int HallId { get; set; }
}