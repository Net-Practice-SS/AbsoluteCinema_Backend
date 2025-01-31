namespace AbsoluteCinema.Application.DTO.Entities;

public class TicketDto
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public int Row { get; set; }
    public int Place { get; set; }
    public int PlacementId { get; set; }
    public int StatusId { get; set; }
}