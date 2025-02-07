namespace AbsoluteCinema.Application.DTO.GenresDTO
{
    public class GetAllGenreDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
