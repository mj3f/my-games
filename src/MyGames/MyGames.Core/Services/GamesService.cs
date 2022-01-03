using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using MyGames.Core.Dtos;
using MyGames.Core.Models;
using Serilog;

namespace MyGames.Core.Services;

/// <summary>
/// Wrapper service to call IGDB to get games data.
/// To get a users games, see UsersService.
/// </summary>
public sealed class GamesService
{
    private static readonly ILogger Logger = Log.ForContext<GamesService>();

    private readonly IMemoryCache _memoryCache;
    private readonly HttpClient _httpClient;

    private const string IgdbApiEndpoint = "https://api.igdb.com/v4/games/";
    
    public GamesService(IMemoryCache cache, HttpClient httpClient)
    {
        _memoryCache = cache;
        _httpClient = httpClient;
    }

    public async Task<GameDto> GetGame(string id)
    {
        TwitchLoginCredentials? loginCredentials = _memoryCache.Get("TwitchLoginCredentials") as TwitchLoginCredentials;
        string? clientId = _memoryCache.Get("ClientId") as string;
        
        if (loginCredentials is null || string.IsNullOrEmpty(clientId))
        {
            return null;
        }

        _httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginCredentials.AccessToken}");

        HttpResponseMessage response =
            await _httpClient.GetAsync(IgdbApiEndpoint + $"{id}?fields=name,cover,genres,platforms");

        response.EnsureSuccessStatusCode();
        
        string responseBody = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(responseBody))
        {
            IgdbGameDto? igdbGame = JsonSerializer.Deserialize<IgdbGameDto>(responseBody);
            Console.WriteLine("what now?");
            
        }

        return null;

    }
    
    
}