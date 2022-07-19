using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyGames.Core.AppSettings;
using MyGames.Database.Interfaces;

namespace MyGames.Core.Repositories;

public sealed class MongoDbRepository<T> : IDbRepository<T> where T : IKeyedObject
{
    private readonly IMongoCollection<T> _collection;
    
    // dbSettings values populated from the appSettings.json file in the myGames.API project.
    public MongoDbRepository(IOptions<MongoDbSettings> dbSettings, string collectionName)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync() ?? new List<T>();

    // public Task<T> GetByIdAsync(string id) => await _collection.Find(x => ??).FirstOrDefaultAsync();

    public async Task<T?> GetByIdAsync(string id) => throw new NotImplementedException();
    
    public async Task CreateAsync(T item) => await _collection.InsertOneAsync(item);
    
    public async Task UpdateAsync(string id, T item)
    {
        var filter = Builders<T>.Filter.Eq(u => u.Key, id);
        await _collection.ReplaceOneAsync(filter, item);
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}