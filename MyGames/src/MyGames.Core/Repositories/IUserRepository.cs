using MyGames.Database.Schemas;

namespace MyGames.Core.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(string username);

    Task AddGameToUsersLibraryAsync(string username, Game game);
    
    Task RemoveGameFromUsersLibraryAsync(string username, string gameId);
    
    Task UpdateGameInUsersLibraryAsync(string username, Game game);

    Task CreateAsync(string username, string password, string salt);
}