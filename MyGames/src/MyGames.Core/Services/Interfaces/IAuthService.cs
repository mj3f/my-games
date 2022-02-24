using MyGames.Core.Dtos;

namespace MyGames.Core.Services.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);

    Task CreateAccountAsync(string username, string password);
}