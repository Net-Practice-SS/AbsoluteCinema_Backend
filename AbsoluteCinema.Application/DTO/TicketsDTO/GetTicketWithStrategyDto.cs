namespace AbsoluteCinema.Application.DTO.TicketsDTO
{
    public class GetTicketWithStrategyDto
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int Row { get; set; }
        public int Place { get; set; }
        public int StatusId { get; set; }
        public double Price { get; set; }

        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
    }
}
