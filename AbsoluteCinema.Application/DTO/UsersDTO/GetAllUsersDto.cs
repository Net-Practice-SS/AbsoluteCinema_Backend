namespace AbsoluteCinema.Application.DTO.UsersDTO;

public class GetAllUsersDto
{
    public string OrderByProperty { get; set; } = "Id";
    public string OrderDirection { get; set; } = "asc";
}