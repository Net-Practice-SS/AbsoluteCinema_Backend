namespace AbsoluteCinema.Application.DTO.ActorsDTO
{

    public class GetActorWithStrategyDto
    {
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
