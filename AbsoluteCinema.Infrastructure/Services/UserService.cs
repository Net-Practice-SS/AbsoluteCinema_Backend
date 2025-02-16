using System.Linq.Dynamic.Core;
using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.UsersDTO;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AbsoluteCinema.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager) {
            
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

     public async Task<IEnumerable<UserDto>> GetAllUsersAsync(GetAllUsersDto getAllUsersDto)
     {
         Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy =
                 query => query.OrderBy($"{getAllUsersDto.OrderByProperty} {getAllUsersDto.OrderDirection}");
             
         var users = await _unitOfWork.Repository<ApplicationUser>().GetAllAsync(orderBy, include: null, page: getAllUsersDto.Page, getAllUsersDto.PageSize);
            
         return _mapper.Map<IEnumerable<UserDto>>(users);
     }
        
     public async Task<UserDto?> GetUserByIdAsync(string id) 
     { 
         var user = await _userManager.FindByIdAsync(id); 
         return user == null ? null : _mapper.Map<UserDto>(user);
     }
        
    public async Task<IdentityResult> UpdateUserAsync(UpdateUserDto updateUserDto) 
    { 
        var user = await _userManager.FindByIdAsync(updateUserDto.Id);
        
        _mapper.Map(updateUserDto, user); 
        
        var result = await _userManager.UpdateAsync(user);
        return result;
    }
        
    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        var result = await _userManager.DeleteAsync(user);
        return result;
    }
}