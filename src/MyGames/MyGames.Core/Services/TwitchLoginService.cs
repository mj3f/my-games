using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Models;
using Serilog;

namespace MyGames.Core.Services;

public sealed class TwitchLoginService
{
    private static readonly ILogger Logger = Log.ForContext<TwitchLoginService>();
    
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    public TwitchLoginService(IOptions<TwitchLoginSettings> loginSettings, HttpClient httpClient, IMemoryCache cache)
    {
        _clientId = loginSettings.Value.ClientId;
        _clientSecret = loginSettings.Value.ClientSecret;

        _httpClient = httpClient;
        _memoryCache = cache;

        _memoryCache.Set("ClientId", _clientId);
    }

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