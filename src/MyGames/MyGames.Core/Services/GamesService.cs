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

    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public GamesService(IMemoryCache cache, HttpClient httpClient)
    {
        _memoryCache = cache;
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<List<IgdbGameDto>?> GetGames()
    {
        SetHeaders();
        HttpResponseMessage response =
            await _httpClient.GetAsync(IgdbApiEndpoint + $"?fields=name,cover,genres,platforms");

        response.EnsureSuccessStatusCode();
        
        string responseBody = await response.Content.ReadAsStringAsync();
        return DeserializeResponseToListOfGames(responseBody);
    }

    public async Task<IgdbGameDto?> GetGame(string id)
    {
        SetHeaders();

        HttpResponseMessage response =
            await _httpClient.GetAsync(IgdbApiEndpoint + $"{id}?fields=name,cover,genres,platforms");

        response.EnsureSuccessStatusCode();
        
        string responseBody = await response.Content.ReadAsStringAsync();
        return DeserializeResponseToListOfGames(responseBody)?.FirstOrDefault();
    }

    private List<IgdbGameDto>? DeserializeResponseToListOfGames(string responseBody)
    {
        if (!string.IsNullOrEmpty(responseBody))
        {
            List<IgdbGameDto>? igdbGames = JsonSerializer.Deserialize<List<IgdbGameDto>>(responseBody, _jsonSerializerOptions);
            return igdbGames;
        }

        return null;
    }

    private void SetHeaders()
    {
        TwitchLoginCredentials? loginCredentials = _memoryCache.Get("TwitchLoginCredentials") as TwitchLoginCredentials;
        string? clientId = _memoryCache.Get("ClientId") as string;
        
        _httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginCredentials?.AccessToken}");
    }

}