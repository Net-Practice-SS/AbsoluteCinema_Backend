namespace AbsoluteCinema.Application.DTO.TicketsDTO;

public class CreateTicketDto
{
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public int Row { get; set; }
    public int Place { get; set; }
    public int StatusId { get; set; }
    public double Price { get; set; }
}