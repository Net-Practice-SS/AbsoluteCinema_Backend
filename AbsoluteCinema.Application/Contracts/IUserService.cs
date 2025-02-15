using AbsoluteCinema.Application.DTO.UsersDTO;
using Microsoft.AspNetCore.Identity;

namespace AbsoluteCinema.Application.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync(GetAllUsersDto getAllUsersDto);
    Task<UserDto?> GetUserByIdAsync(string id);
    Task<IdentityResult> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<IdentityResult> DeleteUserAsync(string id);
}