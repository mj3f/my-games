using MyGames.Core.Dtos;

namespace MyGames.Core.Services.Interfaces;

public interface IUsersService
{
    Task<List<UserDto>> GetUsers();

    Task<UserDto?> GetByUsername(string username);

    Task<bool> AddGameToUsersLibrary(string username, IgdbGameDto gameToAdd);

    Task<bool> RemoveGameFromUsersLibrary(string username, string gameId);

    Task UpdateGameInUsersLibrary(string username, GameDto game);
}