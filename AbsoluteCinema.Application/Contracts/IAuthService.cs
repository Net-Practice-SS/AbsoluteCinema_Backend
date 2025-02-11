using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> SignInAsync(LoginDto user);
        Task<IdentityResult> SignUpAsync(RegisterDto user);
        void LogOut();

    }
}
