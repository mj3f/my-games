using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Dtos;
using MyGames.Core.Services;

namespace MyGames.API.Controllers;

[ApiController]
[Route("api/v0/games")]
[Produces("application/json")]
public sealed class GamesController : ControllerBase
{
    private readonly GamesService _gamesService;

    public GamesController(GamesService gamesService) => _gamesService = gamesService;


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GameDto), 200)]
    [ProducesResponseType(404)]
    [Description("Returns a game entry from the IGDP API.")]
    public async Task<IActionResult> GetGameByIdAsync(string id)
    {
        try
        {
            GameDto game = await _gamesService.GetGame(id);
            return Ok(game);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}