namespace AbsoluteCinema.Application.DTO.HallsDTO
{
    public class GetAllHallDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
