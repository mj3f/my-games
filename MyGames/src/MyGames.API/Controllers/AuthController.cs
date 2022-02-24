using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Dtos;
using MyGames.Core.Services.Interfaces;

namespace MyGames.API.Controllers;

[ApiController]
[Route("api/v0/auth")]
[Produces("application/json")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;
    
    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Description("Will return a jwt token provided a user exists with the username and password inputs provided.")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginCreds)
    {
        if (string.IsNullOrEmpty(loginCreds.Username) || string.IsNullOrEmpty(loginCreds.Password))
        {
            return BadRequest("Username and password required. Check your inputs and try again.");
        }

        bool credentialsValid = await _authService.LoginAsync(loginCreds.Username, loginCreds.Password);

        if (credentialsValid)
        {
            return Ok("jwt token");
        }

        return BadRequest("username or password is invaild.");
        // if (loginCreds.Username == "dummy" && loginCreds.Password == "1234")
        // {
        //     return Ok("Send a token here!");
        // }
        //
        // return BadRequest("Username or password invalid, please check your inputs, then try again.");
    }

    [HttpPost("create")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Description("Creates a new account.")]
    public async Task<IActionResult> CreateAccountAsync([FromBody] LoginDto loginCreds)
    {
        if (string.IsNullOrEmpty(loginCreds.Username) || string.IsNullOrEmpty(loginCreds.Password))
        {
            return BadRequest("Username and password required. Check your inputs and try again.");
        }

        await _authService.CreateAccountAsync(loginCreds.Username, loginCreds.Password);
        return Ok("user created, or should be at least.");
    }
}