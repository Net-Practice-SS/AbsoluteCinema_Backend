using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers {
    public class AuthController : BaseController {

        public readonly IAuthService _authService;
        public readonly IJwtService _jwtService;
        public AuthController(IAuthService authService, IJwtService jwtService) {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] LoginDto user) {
            var result = await _authService.SignInAsync(user);
            if (result.Succeeded) {
                var claims = _jwtService.GetClaims(user.Email);
                var token = _jwtService.CreateJWTToken(claims);
                return Ok(new { result, token });
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> SignUp([FromBody] RegisterDto user) {
            var result = await _authService.SignUpAsync(user);
            if (result.Succeeded) {
                var claims = _jwtService.GetClaims(user.Email);
                var token = _jwtService.CreateJWTToken(claims);
                return Ok(new { result, token });
            }
            return BadRequest(result);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.UserPolicy)]
        public async Task<ActionResult> AssignRoleById([FromBody] AssignRoleByIdDto assignRoleDto)
        {
            var result = await _authService.AssignRoleToUserByIdAsync(assignRoleDto.UserId, assignRoleDto.RoleName);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
