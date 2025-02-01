namespace AbsoluteCinema.Application.DTO.MoviesDTO
{
    public class GetAllMoviesDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
