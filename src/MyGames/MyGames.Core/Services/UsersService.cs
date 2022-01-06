using MongoDB.Driver;
using MyGames.Database.Schemas;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Dtos;
using MyGames.Core.Enums;
using MyGames.Core.Utils;
using Serilog;

namespace MyGames.Core.Services;

/// <summary>
/// Service to fetch user data from the myGames API.
/// </summary>
public sealed class UsersService
{
    private static readonly ILogger Logger = Log.ForContext<UsersService>();
    
    private readonly IMongoCollection<User> _usersCollection;
    
    // dbSettings values populated from the appSettings.json file in the myGames.API project.
    public UsersService(IOptions<MongoDbSettings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            dbSettings.Value.UsersCollectionName);
    }

    /// <summary>
    /// Returns a list of users in the myGames Database.
    /// This method should not be callable by a regular user.
    /// </summary>
    /// <returns>A list of <see cref="UserDto"/></returns>
    public async Task<List<UserDto>> GetUsers()
    {
        var list = await _usersCollection.Find(_ => true).ToListAsync();
        if (list is null || list.Count == 0)
        {
            return new List<UserDto>();
        }
        
        return list.Select(ConvertUserToUserDto).ToList();
    }

    /// <summary>
    /// Gets a user by the unique identifier (id) provided.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <see cref="UserDto"/></returns>
    public async Task<UserDto?> GetById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return null;
        }
        
        var user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        if (user is null)
        {
            return null;
        }
        
        return ConvertUserToUserDto(user);
    }

    /// <summary>
    /// Gets a user by the unique identifier (user provided username) provided.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>A <see cref="UserDto"/></returns>
    public async Task<UserDto?> GetByUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return null;
        }
        
        var user = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (user is null)
        {
            return null;
        }
        
        return ConvertUserToUserDto(user);
    }

    /// <summary>
    /// Adds a game to a users library.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="gameToAdd"></param>
    public async Task AddGameToUsersLibrary(string username, IgdbGameDto gameToAdd)
    {
        if (string.IsNullOrEmpty(username))
        {
            return;
        }
        
        // Convert the igdb game to a Game (mongodb schema).
        var game = new Game
        {
            Id = GuidGenerator.GenerateGuidForMongoDb(),
            IgdbId = gameToAdd.Id,
            Name = gameToAdd.Name,
            CoverArtUrl = gameToAdd.CoverArtUrl,
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

    /// <summary>
    /// Removes a game from the users library if it exists.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="gameId"></param>
    public async Task RemoveGameFromUsersLibrary(string username, string gameId)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(gameId))
        {
            return;
        }
        
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

    /// <summary>
    /// Converts a user schema returned from the mongoDB users collection into a User Dto.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A <see cref="UserDto"/></returns>
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
            CoverArtUrl = g.CoverArtUrl,
            Notes = g.Notes.Select(n => new GameNoteDto
            {
                Id = n.Id!,
                Content = n.Content,
                CreatedAt = n.CreatedAt
            }).ToList()
        }).ToList()
    };
}