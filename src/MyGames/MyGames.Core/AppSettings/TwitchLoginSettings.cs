namespace MyGames.Core.AppSettings;

/// <summary>
/// Mapping of string values needed for making API requests to IGDB, found in appSettings.json file of the API project.
/// </summary>
public sealed class TwitchLoginSettings
{
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
}