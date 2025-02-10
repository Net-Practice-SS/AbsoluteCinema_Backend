namespace AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO
{
    public class GetAllSessionDto
    {
        public string OrderByProperty { get; set; } = "Id";
        public string OrderDirection { get; set; } = "desc";
    }
}
