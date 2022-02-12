using MyGames.Core.Dtos;

namespace MyGames.Core.Services.Interfaces;

public interface IUsersService
{
    Task<List<UserDto>> GetUsers();

    Task<UserDto?> GetById(string id);

    Task<UserDto?> GetByUsername(string username);

    Task AddGameToUsersLibrary(string username, IgdbGameDto gameToAdd);

    Task RemoveGameFromUsersLibrary(string username, string gameId);

    Task UpdateGameInUsersLibrary(string username, GameDto game);
}