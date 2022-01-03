using MongoDB.Driver;
using MyGames.Database;
using MyGames.Database.Schemas;
using Microsoft.Extensions.Options;
using MyGames.Core.Dtos;

namespace MyGames.Core.Services;

public sealed class UsersService
{
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
        return list.Select(ConvertUserToUserDto).ToList();
    }

    public async Task<UserDto?> GetById(string id)
    {
        var user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        return ConvertUserToUserDto(user);
    }

    public async Task<UserDto?> GetByUsername(string username)
    {
        var user = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        return ConvertUserToUserDto(user);
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