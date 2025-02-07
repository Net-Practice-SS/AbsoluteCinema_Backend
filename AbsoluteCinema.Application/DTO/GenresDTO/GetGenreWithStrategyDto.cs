namespace AbsoluteCinema.Application.DTO.GenresDTO
{
    public class GetGenreWithStrategyDto
    {
        public string? Title { get; set; } = null!;
        public List<int> MoviesIds { get; set; } = new();
        
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
