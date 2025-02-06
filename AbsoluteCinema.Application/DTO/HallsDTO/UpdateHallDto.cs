namespace AbsoluteCinema.Application.DTO.HallsDTO
{
    public class UpdateHallDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public int? RowCount { get; set; } = null!;
        public int? PlaceCount { get; set; } = null!;
    }
}
