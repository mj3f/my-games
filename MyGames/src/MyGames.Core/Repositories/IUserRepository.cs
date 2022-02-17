using MyGames.Database.Schemas;

namespace MyGames.Core.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(string id);

    Task AddGameToUsersLibraryAsync(string username, Game game);
    
    Task RemoveGameFromUsersLibraryAsync(string username, string gameId);
    
    Task UpdateGameInUsersLibraryAsync(string username, Game game);

    // Task CreateAsync(T obj);
}