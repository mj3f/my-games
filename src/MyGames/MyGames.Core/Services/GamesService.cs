using MyGames.Core.Dtos;
using MyGames.Core.Models;

namespace MyGames.Core.Services;

/// <summary>
/// Wrapper service to call IGDB to get games data.
/// To get a users games, see UsersService.
/// </summary>
public sealed class GamesService
{
    private readonly TwitchLoginService _twitchLoginService;
    private readonly HttpClient _httpClient;

    private const string IgdbApiEndpoint = "https://api.igdb.com/v4/games/";
    
    public GamesService(TwitchLoginService twitchLoginService, HttpClient httpClient)
    {
        _twitchLoginService = twitchLoginService;
        _httpClient = httpClient;
    }

    public async Task<GameDto> GetGame(string id)
    {
        TwitchLoginCredentials? loginCredentials = _twitchLoginService.TwitchLoginCredentials;
        
        if (loginCredentials is null)
        {
            _twitchLoginService.Login();
        }
        
        _httpClient.DefaultRequestHeaders.Add("Client-ID", _twitchLoginService.ClientId);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginCredentials.AccessToken}");

        HttpResponseMessage response =
            await _httpClient.GetAsync(IgdbApiEndpoint + $"{id}?fields=name,cover,genres,platforms");

        response.EnsureSuccessStatusCode();
        
        string responseBody = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(responseBody))
        {
            // TwitchLoginCredentials = JsonSerializer.Deserialize<TwitchLoginCredentials>(responseBody);
        }

        return null;

    }
    
    
}