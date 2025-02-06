namespace AbsoluteCinema.Application.DTO.GenresDTO
{
    public class GetGenreWithStrategyDto
    {
        public string? Title { get; set; } = null!;

        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
