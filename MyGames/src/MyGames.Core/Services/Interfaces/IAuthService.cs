using MyGames.Core.Dtos;

namespace MyGames.Core.Services.Interfaces;

public interface IAuthService
{
    Task<bool> VerifyCredentialsAsync(string username, string password);

    Task<string> LoginAsync(string username);

    Task CreateAccountAsync(string username, string password);
}