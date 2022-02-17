using MyGames.Core.Dtos;

namespace MyGames.Core.Services.Interfaces;

public interface IGamesService
{
    Task<List<IgdbGameDto>?> GetGames(string? name = null);

    Task<IgdbGameDto?> GetGame(int id);
}