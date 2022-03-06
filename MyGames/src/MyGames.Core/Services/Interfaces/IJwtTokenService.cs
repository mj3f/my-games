namespace MyGames.Core.Services.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateToken(string userId);

    Task<string> GenerateRefreshToken();
}