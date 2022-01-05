using System.Text.Json.Serialization;

namespace MyGames.Core.Models;

/// <summary>
/// POCO model of auth data returned by the twitch API upon successful login.
/// </summary>
public sealed class TwitchLoginCredentials
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
    
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;
}