using System.ComponentModel;
using IGDB.Models;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Dtos;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;

namespace MyGames.API.Controllers;

[ApiController]
[Route("api/v0/games")]
[Produces("application/json")]
public sealed class IdgbGamesController : ControllerBase
{
    private readonly IGamesService _gamesService;

    public IdgbGamesController(IGamesService gamesService) => _gamesService = gamesService;

    [HttpGet]
    [ProducesResponseType(typeof(List<IgdbGameDto>), 200)]
    [ProducesResponseType(404)]
    [Description("Returns a list of game entries from the IGDP API.")]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? name)
    {
        try
        {
            List<IgdbGameDto>? games = await _gamesService.GetGames(name);
            
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
    [ProducesResponseType(400)]
    [Description("Returns a game entry from the IGDP API.")]
    public async Task<IActionResult> GetByIdAsync(int id)
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
            return BadRequest(ex.Message);
        }
    }
}