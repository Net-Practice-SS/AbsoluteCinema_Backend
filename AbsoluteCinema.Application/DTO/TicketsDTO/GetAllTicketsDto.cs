namespace AbsoluteCinema.Application.DTO.TicketsDTO
{
    public class GetAllTicketsDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "desc";
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
