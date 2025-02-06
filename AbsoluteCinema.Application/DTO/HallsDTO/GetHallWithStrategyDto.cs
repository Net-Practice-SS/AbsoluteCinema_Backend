namespace AbsoluteCinema.Application.DTO.HallsDTO
{
    public class GetHallWithStrategyDto
    {
        public string? Name { get; set; } = null!;
        public int? RowCount { get; set; } = null!;
        public int? PlaceCount { get; set; } = null!;

        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
