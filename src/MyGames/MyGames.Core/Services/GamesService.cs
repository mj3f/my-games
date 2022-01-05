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
    private const string IgdbApiEndpoint = "https://api.igdb.com/v4/games/";
    
    private readonly IMemoryCache _memoryCache;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public GamesService(IMemoryCache cache, HttpClient httpClient)
    {
        _memoryCache = cache;
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    /// <summary>
    /// Get a collection of games from the IGDB API.
    /// </summary>
    /// <returns>A list of <see cref="IgdbGameDto"/></returns>
    public async Task<List<IgdbGameDto>?> GetGames()
    {
        SetHeaders();
        
        HttpResponseMessage response =
            await _httpClient.GetAsync(IgdbApiEndpoint + $"?fields=name,cover,genres,platforms");

        response.EnsureSuccessStatusCode();
        
        string responseBody = await response.Content.ReadAsStringAsync();
        return DeserializeResponseToListOfGames(responseBody);
    }

    /// <summary>
    /// Get a game from the IGDB API based on a unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <see cref="IgdbGameDto"/></returns>
    public async Task<IgdbGameDto?> GetGame(string id)
    {

        if (string.IsNullOrEmpty(id))
        {
            return null;
        }
        
        SetHeaders();

        HttpResponseMessage response =
            await _httpClient.GetAsync(IgdbApiEndpoint + $"{id}?fields=name,cover,genres,platforms");

        response.EnsureSuccessStatusCode();
        
        string responseBody = await response.Content.ReadAsStringAsync();
        return DeserializeResponseToListOfGames(responseBody)?.FirstOrDefault();
    }

    /// <summary>
    /// Takes the json string response from the IGDB API, and deserializes it to a list of type IgdbGameDto.
    /// If no games returned, this method will return null to the controller endpoint.
    /// </summary>
    /// <param name="responseBody"></param>
    /// <returns>A list of <see cref="IgdbGameDto"/></returns>
    private List<IgdbGameDto>? DeserializeResponseToListOfGames(string responseBody)
    {
        if (!string.IsNullOrEmpty(responseBody))
        {
            List<IgdbGameDto>? igdbGames = JsonSerializer.Deserialize<List<IgdbGameDto>>(responseBody, _jsonSerializerOptions);
            return igdbGames;
        }

        return null;
    }

    /// <summary>
    /// Sets two custom Http requesst headers that required for making requests to the IGDB API.
    /// Client-ID, which is the id of the registered Twitch developer account, and a Bearer authorization token.
    /// </summary>
    private void SetHeaders()
    {
        TwitchLoginCredentials? loginCredentials = _memoryCache.Get("TwitchLoginCredentials") as TwitchLoginCredentials;
        string? clientId = _memoryCache.Get("ClientId") as string;
        
        _httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginCredentials?.AccessToken}");
    }

}