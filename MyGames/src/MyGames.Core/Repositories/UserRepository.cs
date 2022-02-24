using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyGames.Core.AppSettings;
using MyGames.Core.Utils;
using MyGames.Database.Schemas;

namespace MyGames.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _usersCollection;
    
    // dbSettings values populated from the appSettings.json file in the myGames.API project.
    public UserRepository(IOptions<MongoDbSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            dbSettings.Value.UsersCollectionName);
    }
    
    public async Task<List<User>> GetAllAsync() => await _usersCollection.Find(_ => true).ToListAsync() ?? new List<User>();

    public async Task<User?> GetByIdAsync(string id) => await _usersCollection.Find(u => u.Username == id).FirstOrDefaultAsync();

    public async Task AddGameToUsersLibraryAsync(string username, Game game)
    {
        await _usersCollection.FindOneAndUpdateAsync(
            u => u.Username == username, // TODO: How to handle case where username does not match a user in the db?
            Builders<User>.Update.AddToSet(u => u.Games, game));
    }

    public async Task RemoveGameFromUsersLibraryAsync(string username, string gameId)
    {
        // https://stackoverflow.com/a/30145663
        var update = Builders<User>.Update.PullFilter(u => u.Games,
            g => g.Id == gameId);
        
        await _usersCollection.FindOneAndUpdateAsync(u => u.Username == username, update);
    }

    public async Task UpdateGameInUsersLibraryAsync(string username, Game game)
    {
        User user = _usersCollection
            .AsQueryable()
            .First(u => u.Username == username);

        Game existingGame = user.Games.First(g => g.Id == game.Id);
        existingGame.Status = game.Status;

        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        await _usersCollection.ReplaceOneAsync(filter, user);
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
        
        await _usersCollection.InsertOneAsync(user);
    }
}