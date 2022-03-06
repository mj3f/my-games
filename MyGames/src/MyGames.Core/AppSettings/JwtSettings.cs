namespace MyGames.Core.AppSettings;

public sealed class JwtSettings
{
    public string Audience { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string SecretKey { get; set; } = null!;
}