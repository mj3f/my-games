namespace MyGames.Core.AppSettings;

/// <summary>
/// Mapping of jwt settings, found in appsettings.json.
/// </summary>
public sealed class JwtSettings
{
    public string Audience { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string SecretKey { get; set; } = null!;
}