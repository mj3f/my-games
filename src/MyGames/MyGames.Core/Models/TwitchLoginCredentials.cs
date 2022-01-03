using System.Text.Json.Serialization;

namespace MyGames.Core.Models;

public sealed class TwitchLoginCredentials
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
    
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;
}