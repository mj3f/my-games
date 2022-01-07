using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Dtos;

namespace MyGames.API.Controllers;

[ApiController]
[Route("api/v0/auth")]
[Produces("application/json")]
public sealed class AuthController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Description("Will return a jwt token provided a user exists with the username and password inputs provided.")]
    public IActionResult LoginAsync([FromBody] LoginDto loginCreds)
    {
        if (loginCreds.Username == "dummy" && loginCreds.Password == "1234")
        {
            return Ok("Send a token here!");
        }

        return BadRequest("Username or password invalid, please check your inputs, then try again.");
    }
}