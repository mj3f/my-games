using MongoDB.Driver;
using MyGames.Database.Schemas;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Dtos;
using MyGames.Core.Enums;
using MyGames.Core.Utils;
using Serilog;

namespace MyGames.Core.Services;

public sealed class UsersService
{
    private static readonly ILogger Logger = Log.ForContext<UsersService>();
    
    private readonly IMongoCollection<User> _usersCollection;
    
    public UsersService(IOptions<MongoDbSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            dbSettings.Value.UsersCollectionName);
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var list = await _usersCollection.Find(_ => true).ToListAsync();
        if (list is null || list.Count == 0)
        {
            return new List<UserDto>();
        }
        
        return list.Select(ConvertUserToUserDto).ToList();
    }

    public async Task<UserDto?> GetById(string id)
    {
        var user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        if (user is null)
        {
            return null;
        }
        
        return ConvertUserToUserDto(user);
    }

    public async Task<UserDto?> GetByUsername(string username)
    {
        var user = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (user is null)
        {
            return null;
        }
        
        return ConvertUserToUserDto(user);
    }

    public async Task AddGameToUsersLibrary(string username, IgdbGameDto gameToAdd)
    {
        // Convert the igdb game to a Game (mongodb schema).
        var game = new Game
        {
            Id = GuidGenerator.GenerateGuidForMongoDb(),
            IgdbId = gameToAdd.Id,
            Name = gameToAdd.Name,
            Notes = new List<GameNote>(),
            Status = GameStatus.Backlog
        };

        try
        {
            await _usersCollection.FindOneAndUpdateAsync(
                u => u.Username == username, // TODO: How to handle case where username does not match a user in the db?
                Builders<User>.Update.AddToSet(u => u.Games, game));
        }
        catch (Exception ex)
        {
            Logger.Error("[UsersService] Error occurred whilst trying to add a game to a users library. " + ex.Message);
            throw;
        }
    }

    public async Task RemoveGameFromUsersLibrary(string username, string gameId)
    {
        try
        {
            // https://stackoverflow.com/a/30145663
            var update = Builders<User>.Update.PullFilter(u => u.Games,
                g => g.Id == gameId);
            
            var result = await _usersCollection.FindOneAndUpdateAsync(u => u.Username == username, update);
        }
        catch (Exception ex)
        {
            Logger.Error("[UsersService] Error occurred whilst trying to remove a game from a users library. " + ex.Message);
            throw;
        }
    }

    private UserDto ConvertUserToUserDto(User user) => new UserDto
    {
        Id = user.Id!,
        Username = user.Username,
        Games = user.Games.Select(g => new GameDto
        {
            Id = g.Id!,
            Name = g.Name,
            GameStatus = g.Status,
            IgdbId = g.IgdbId,
            Notes = g.Notes.Select(n => new GameNoteDto
            {
                Id = n.Id!,
                Content = n.Content,
                CreatedAt = n.CreatedAt
            }).ToList()
        }).ToList()
    };
}