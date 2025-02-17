using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.UsersDTO;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetUserById(string id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null)
            return NotFound();
        return Ok(userDto);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllUsers([FromQuery]GetAllUsersDto getAllUsersDto)
    {
        var users = await _userService.GetAllUsersAsync(getAllUsersDto);
        return Ok(users);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        var result = await _userService.UpdateUserAsync(updateUserDto);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
}