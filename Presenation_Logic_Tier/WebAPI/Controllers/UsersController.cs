using Application.LogicInterfaces;
using Domain.DTOs.UserDTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    [HttpPost]
    [Authorize("MustBeAdmin")]
    public async Task<ActionResult<User>> RegisterUser(UserCreationDto dto)
    {
        Console.WriteLine("REGISTERING THE USER");
        try
        {
            
            User createdUser = await _userLogic.CreateUserAsync(dto);
            return Created($"users/{createdUser.Username}",createdUser);
        }
        catch (Exception e)
        {
            
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{username}")]
    [Authorize("MustBeAdmin")]
    public async Task<ActionResult<User>> GetByUsernameAsync([FromRoute] string username)
    {   
        Console.WriteLine("getting user details");
        try
        {
            User existingUser = await _userLogic.GetUserByUsernameAsync(username);
            return Ok(existingUser);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
}