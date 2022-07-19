using MyGames.Database.Interfaces;

namespace MyGames.Core.Repositories;

public interface IDbRepository<T> where T : IKeyedObject
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
    Task CreateAsync(T item);
    Task UpdateAsync(string id, T item);
    Task DeleteAsync(string id);
}