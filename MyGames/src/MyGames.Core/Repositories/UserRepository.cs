using MyGames.Core.Utils;
using MyGames.Database.Schemas;

namespace MyGames.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbRepository<User> _dbRepository;
    
    public UserRepository(IDbRepository<User> dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<List<User>> GetAllAsync() => await _dbRepository.GetAllAsync();

    public async Task<User?> GetByIdAsync(string id) => await _dbRepository.GetByIdAsync(id);

    public async Task AddGameToUsersLibraryAsync(string username, Game game)
    {
        User? user = await _dbRepository.GetByIdAsync(username);
        if (user is null)
        {
            return;
        }
        
        user.Games!.Add(game);
        await _dbRepository.UpdateAsync(username, user);
        
        // TODO: Does the above work the same as this?
        // await _usersCollection.FindOneAndUpdateAsync(
        //     u => u.Username == username,
        //     Builders<User>.Update.AddToSet(u => u.Games, game));
        
    }

    public async Task RemoveGameFromUsersLibraryAsync(string username, string gameId)
    {
        User? user = await GetByIdAsync(username);

        if (user is null)
        {
            return;
        }

        Game? gameToRemove = user.Games!.FirstOrDefault(g => g.Id == gameId);

        if (gameToRemove is null)
        {
            return;
        }

        user.Games!.Remove(gameToRemove);
        
        await _dbRepository.UpdateAsync(username, user);

        // TODO: Does the above work the same as this?
        // https://stackoverflow.com/a/30145663
        // var update = Builders<User>.Update.PullFilter(u => u.Games,
        //     g => g.Id == gameId);
        //
        // await _usersCollection.FindOneAndUpdateAsync(u => u.Username == username, update);
    }

    public async Task UpdateGameInUsersLibraryAsync(string username, Game game)
    {
        User? user = await GetByIdAsync(username);

        if (user is null)
        {
            return;
        }

        Game? existingGame = user.Games!.FirstOrDefault(g => g.Id == game.Id);
        if (existingGame is null)
        {
            return;
        }

        existingGame.Status = game.Status;

        await _dbRepository.UpdateAsync(username, user);

        // TODO: Does the above work the same as this?
        // var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        // await _usersCollection.ReplaceOneAsync(filter, user);
    }

    public async Task CreateAsync(string username, string password, string salt)
    {
        var user = new User
        {
            Username = username,
            Password = password,
            Salt = salt,
            Id = GuidGenerator.GenerateGuidForMongoDb(),
            Games = new List<Game>()
        };

        await _dbRepository.CreateAsync(user);
        
        // await _usersCollection.InsertOneAsync(user);
    }
}