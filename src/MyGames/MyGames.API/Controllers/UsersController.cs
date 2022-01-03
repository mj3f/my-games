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

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        UserDto? user = await _usersService.GetByUsername(username);

        return user is not null ? Ok(user) : NotFound("No user with this username found");
    }
}