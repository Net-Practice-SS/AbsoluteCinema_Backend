namespace AbsoluteCinema.Application.DTO.Entities;

public class HallDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? RowCount { get; set; }
    public int? PlaceCount { get; set; }
}