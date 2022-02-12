using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Dtos;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;

namespace MyGames.API.Controllers;

[ApiController]
[Route("api/v0/users")]
[Produces("application/json")]
public sealed class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService) => _usersService = usersService;
    
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

    [HttpPut("{username}/game")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Description("Updates a game in a users library.")]
    public async Task<IActionResult> UpdateGameAsync(string username, [FromBody] GameDto game)
    {
        try
        {
            await _usersService.UpdateGameInUsersLibrary(username, game);
            return Ok("game updated.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{username}/add-game")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Description("Adds a game to a users library.")]
    public async Task<IActionResult> AddGameToUserLibraryAsync(string username, [FromBody] IgdbGameDto game)
    {
        try
        {
            await _usersService.AddGameToUsersLibrary(username, game);
            return Ok("game added to users library");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{username}/remove-game")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Description("Removes a game from a users library.")]
    public async Task<IActionResult> RemoveGameFromUserLibraryAsync(string username, [FromQuery] string gameId)
    {
        try
        {
            await _usersService.RemoveGameFromUsersLibrary(username, gameId);
            return Ok("game removed from users library");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}