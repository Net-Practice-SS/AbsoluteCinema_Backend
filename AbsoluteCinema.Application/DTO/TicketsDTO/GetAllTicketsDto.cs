namespace AbsoluteCinema.Application.DTO.TicketsDTO
{
    public class GetAllTicketsDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "desc";
    }
}
