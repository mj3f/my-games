using System.Text.Json;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Models;
using Serilog;

namespace MyGames.Core.Services;

public sealed class TwitchLoginService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private static readonly ILogger Logger = Log.ForContext<TwitchLoginService>();
    
    public TwitchLoginCredentials? TwitchLoginCredentials { get; private set; }
    
    public TwitchLoginService(IOptions<TwitchLoginSettings> loginSettings)
    {
        _clientId = loginSettings.Value.ClientId;
        _clientSecret = loginSettings.Value.ClientSecret;
    }

    public async Task Login()
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(
                    $"https://id.twitch.tv/oauth2/token?client_id={_clientId}" +
                    $"&client_secret={_clientSecret}&grant_type=client_credentials", null);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                {
                    TwitchLoginCredentials = JsonSerializer.Deserialize<TwitchLoginCredentials>(responseBody);
                }
                
                Logger.Information("[TwitchLoginService] logged into twitch.");
            }
        }
        catch (HttpRequestException ex)
        {
            Logger.Error("[TwitchLoginService] Login to twitch failed.");
        }
        
    }
}