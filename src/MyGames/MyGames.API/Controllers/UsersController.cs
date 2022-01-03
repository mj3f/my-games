using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Dtos;
using MyGames.Core.Services;

namespace MyGames.API.Controllers;

[ApiController]
[Route("api/v0/users")]
[Produces("application/json")]
public sealed class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService) => _usersService = usersService;
    
    [HttpGet]
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    [ProducesResponseType(400)]
    [Description("Returns a list of all users.")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            List<UserDto> users = await _usersService.GetUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{username}")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(404)]
    [Description("Returns a user by passing in its username.")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        UserDto? user = await _usersService.GetByUsername(username);

        return user is not null ? Ok(user) : NotFound("No user with this username found");
    }
    
    [HttpPost]
    [ProducesResponseType( 200)]
    [ProducesResponseType(400)]
    [Description("Creates a new user and saves it to the db.")]
    public async Task<IActionResult> CreateAsync(string username)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{username}/add-game")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Description("Adds a game to a users library.")]
    public async Task<IActionResult> CreateAsync(string username, [FromBody] GameDto game)
    {
        throw new NotImplementedException();
    }
}