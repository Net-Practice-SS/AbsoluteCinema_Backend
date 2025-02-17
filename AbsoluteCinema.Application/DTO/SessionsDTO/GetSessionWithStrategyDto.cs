namespace AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO
{
    public class GetSessionWithStrategyDto
    {
        public int? MovieId { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public int? HallId { get; set; } = null!;

        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "asc";
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
