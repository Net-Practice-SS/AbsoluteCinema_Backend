using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AbsoluteCinema.Domain.Exceptions;

namespace AbsoluteCinema.Infrastructure.Services {
    public class JWTService : IJwtService {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;


        public JWTService(IConfiguration configuration, UserManager<ApplicationUser> userManager) {
            _configuration = configuration;
            _userManager = userManager;
        }

        public string CreateJWTToken(IEnumerable<Claim> claims) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:TokenIssuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenLifeMinutes"])),
                signingCredentials: signIn
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(string email) {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(ApplicationUser), "email", email);
            }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("UserId", user.Id.ToString())
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            return claims;
        }


    }
}
