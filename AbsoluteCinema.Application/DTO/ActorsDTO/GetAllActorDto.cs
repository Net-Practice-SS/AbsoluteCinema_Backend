namespace AbsoluteCinema.Application.DTO.ActorsDTO
{
    public class GetAllActorDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
