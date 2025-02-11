using AbsoluteCinema.Application.DTO.AuthDTO;
using Microsoft.AspNetCore.Identity;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> SignInAsync(LoginDto user);
        Task<IdentityResult> SignUpAsync(RegisterDto user);
    }
}
