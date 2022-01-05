using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Models;
using Serilog;

namespace MyGames.Core.Services;

/// <summary>
/// Service to authenticate with Twitch/IGDB API prior to making requests.
/// </summary>
public sealed class TwitchLoginService
{
    private static readonly ILogger Logger = Log.ForContext<TwitchLoginService>();
    
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    // loginSettings values populated from appSettings.json in API project.
    public TwitchLoginService(IOptions<TwitchLoginSettings> loginSettings, HttpClient httpClient, IMemoryCache cache)
    {
        _clientId = loginSettings.Value.ClientId;
        _clientSecret = loginSettings.Value.ClientSecret;

        _httpClient = httpClient;
        _memoryCache = cache;

        _memoryCache.Set("ClientId", _clientId); // Globally cache client id so it can be accessed elsewhere.
    }

    /// <summary>
    /// Authenticate with the Twitch developer API in order to make API requests to IGDB to get game data etc.
    /// This method is called periodically inside a background service to refresh the bearer token.
    /// </summary>
    public async Task Login()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync(
                    $"https://id.twitch.tv/oauth2/token?client_id={_clientId}" +
                    $"&client_secret={_clientSecret}&grant_type=client_credentials", null);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                {
                    var creds = JsonSerializer.Deserialize<TwitchLoginCredentials>(responseBody);
                    _memoryCache.Set("TwitchLoginCredentials", creds);
                }
                
                Logger.Information("[TwitchLoginService] logged into twitch.");
        }
        catch (HttpRequestException ex)
        {
            Logger.Error("[TwitchLoginService] Login to twitch failed.");
        }
        
    }
}