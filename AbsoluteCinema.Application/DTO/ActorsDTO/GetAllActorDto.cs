namespace AbsoluteCinema.Application.DTO.ActorsDTO
{
    public class GetAllActorDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
