namespace AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO
{
    public class GetAllSessionDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "desc";
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
