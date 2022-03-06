using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyGames.Core.AppSettings;
using MyGames.Core.Services.Interfaces;

namespace MyGames.Core.Services;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenService(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value;
    }

    public async Task<string> GenerateToken(string userId)
    {
        var key = Convert.FromBase64String(_jwtSettings.SecretKey);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        });

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        string jwtToken = await Task.Run(() => tokenHandler.WriteToken(securityToken));
        return jwtToken;
    }

    public async Task<string> GenerateRefreshToken()
    {
        var randomBytes = new byte[32];

        using var randomNumberGenerator = RandomNumberGenerator.Create(); // non static instance.

        await Task.Run(() => randomNumberGenerator.GetBytes(randomBytes));

        string refreshToken = Convert.ToBase64String(randomBytes);

        return refreshToken;
    }
}