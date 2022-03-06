using MyGames.Core.Repositories;
using MyGames.Core.Services.Interfaces;
using MyGames.Core.Utils;
using MyGames.Database.Schemas;

namespace MyGames.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IUserRepository repository, IJwtTokenService jwtTokenService)
    {
        _repository = repository;
        _jwtTokenService = jwtTokenService;
    }
    
    public async Task<bool> VerifyCredentialsAsync(string username, string password)
    {
        User? user = await _repository.GetByIdAsync(username);
        if (user is not null)
        { 
            return PasswordHasher.VerifyHashedPassword(user.Password, password, user.Salt);
        }

        return false;
    }

    public async Task<string> LoginAsync(string username)
    {
        return await _jwtTokenService.GenerateToken(username);
    }

    public async Task CreateAccountAsync(string username, string password)
    {
        (string hashedAndSaltedPassword, string salt) = PasswordHasher.HashAndSaltPassword(password);
        await _repository.CreateAsync(username, hashedAndSaltedPassword, salt);
    }

   
}