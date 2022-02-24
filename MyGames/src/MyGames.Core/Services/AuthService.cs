using MyGames.Core.Repositories;
using MyGames.Core.Services.Interfaces;
using MyGames.Core.Utils;
using MyGames.Database.Schemas;

namespace MyGames.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;

    public AuthService(IUserRepository repository) => _repository = repository;
    
    public async Task<bool> LoginAsync(string username, string password)
    {
        User? user = await _repository.GetByIdAsync(username);
        if (user is not null)
        { 
            return PasswordHasher.VerifyHashedPassword(user.Password, password, user.Salt);
        }

        return false;
    }

    public async Task CreateAccountAsync(string username, string password)
    {
        (string hashedAndSaltedPassword, string salt) = PasswordHasher.HashAndSaltPassword(password);
        await _repository.CreateAsync(username, hashedAndSaltedPassword, salt);
    }

   
}