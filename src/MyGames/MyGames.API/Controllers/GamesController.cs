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

    [HttpGet]
    [ProducesResponseType(typeof(List<IgdbGameDto>), 200)]
    [ProducesResponseType(404)]
    [Description("Returns a list of game entries from the IGDP API.")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            List<IgdbGameDto>? games = await _gamesService.GetGames();
            
            if (games is null)
            {
                return NotFound("No games found, odd..");
            }
            
            return Ok(games);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IgdbGameDto), 200)]
    [ProducesResponseType(404)]
    [Description("Returns a game entry from the IGDP API.")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            IgdbGameDto? game = await _gamesService.GetGame(id);
            
            if (game is null)
            {
                return NotFound("No game found for this id");
            }
            
            return Ok(game);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}