using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AbsoluteCinema.Infrastructure.Services {
    public class AuthService : IAuthService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void LogOut() {
            throw new NotImplementedException();
        }
        public async Task<IdentityResult> SignInAsync(LoginDto userLoginDto) {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null) {
                var emailError = new IdentityError {
                    Code = "NonExistantEmail",
                    Description = $"The email '{userLoginDto.Email}' is not signed up."
                };
                return IdentityResult.Failed(emailError);
            }

            var matches = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            if (matches) {
                return IdentityResult.Success;
            } else {
                var passwordError = new IdentityError {
                    Code = "WrongPassword",
                    Description = $"Password for user with email {userLoginDto.Email} is incorrect."
                };
                return IdentityResult.Failed(passwordError);
            }
        }


        public async Task<IdentityResult> SignUpAsync(RegisterDto userRegisterDto) {
            var user = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            string userRole = "User";

            if (user != null) {
                var DublicateEmailError = new IdentityError {
                    Code = "DuplicateEmail",
                    Description = $"The email '{userRegisterDto.Email}' is already signed up."
                };
                return IdentityResult.Failed(DublicateEmailError);
            }

            var newUser = _mapper.Map<ApplicationUser>(userRegisterDto);
            if (await _roleManager.RoleExistsAsync(userRole)) {
                var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
                if( result.Succeeded ) {
                        var addRoleResult = await _userManager.AddToRoleAsync(newUser, userRole);
                        return addRoleResult;
                    }
                return result;
            }
            var RoleDoesNotExistError= new IdentityError {
                Code = "RoleDoesNotExist",
                Description = $"Role {userRole} does not exist."
            };
            return IdentityResult.Failed(RoleDoesNotExistError);


        }
    }
}
