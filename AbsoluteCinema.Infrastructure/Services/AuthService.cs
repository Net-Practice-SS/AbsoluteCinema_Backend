using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AbsoluteCinema.Domain.Constants;

namespace AbsoluteCinema.Infrastructure.Services {
    public class AuthService : IAuthService {
        
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) {
            
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<IdentityResult> SignInAsync(LoginDto userLoginDto) {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null) {
                var emailError = new IdentityError {
                    Code = "NonExistentEmail",
                    Description = $"The email '{userLoginDto.Email}' is not signed up."
                };
                return IdentityResult.Failed(emailError);
            }

            var matches = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            
            if (matches) {
                return IdentityResult.Success;
            } 
            else {
                var passwordError = new IdentityError {
                    Code = "WrongPassword",
                    Description = $"Password for user with email {userLoginDto.Email} is incorrect."
                };
                return IdentityResult.Failed(passwordError);
            }
        }
        
        public async Task<IdentityResult> SignUpAsync(RegisterDto userRegisterDto) {
            var user = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            var userRole = Role.User;

            if (user != null) {
                var duplicateEmailError = new IdentityError {
                    Code = "DuplicateEmail",
                    Description = $"The email '{userRegisterDto.Email}' is already signed up."
                };
                return IdentityResult.Failed(duplicateEmailError);
            }

            var newUser = _mapper.Map<ApplicationUser>(userRegisterDto);
            
            if (await _roleManager.RoleExistsAsync(userRole)) {
                
                var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
                
                var roleEntity = await _roleManager.FindByNameAsync(userRole);
                
                if( result.Succeeded ) {
                    if (roleEntity?.Name != null)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(newUser, roleEntity.Name);
                        return addRoleResult;
                    }
                }
                return result;
            }
            
            var roleDoesNotExistError= new IdentityError {
                Code = "RoleDoesNotExist",
                Description = $"Role {userRole} does not exist."
            };
            return IdentityResult.Failed(roleDoesNotExistError);
        }
        
        public async Task<IdentityResult> AssignRoleToUserByIdAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"User with ID '{userId}' not found."
                });
            }
            
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = $"Role '{roleName}' does not exist."
                });
            }
            
            var hasRole = await _userManager.IsInRoleAsync(user, roleName);
            if (hasRole)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleAlreadyAssigned",
                    Description = $"User with ID '{userId}' already has the role '{roleName}'."
                });
            }
            var currentRole = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRole);
            
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }
    }
}
