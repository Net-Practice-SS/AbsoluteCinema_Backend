namespace AbsoluteCinema.Application.DTO.HallsDTO
{
    public class CreateHallDto
    {
        public string Name { get; set; } = null!;
        public int RowCount { get; set; }
        public int PlaceCount { get; set; }
    }
}
