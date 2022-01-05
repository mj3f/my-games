using System.Text.Json;
using IGDB;
using IGDB.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
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
    private const string IgdbApiEndpoint = "https://api.igdb.com/v4/";
    
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private readonly IGDBClient _client;
    
    public GamesService(IOptions<TwitchLoginSettings> loginSettings, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        _client = CreateIGDBClient(loginSettings.Value.ClientId, loginSettings.Value.ClientSecret);
    }

    /// <summary>
    /// Get a collection of games from the IGDB API.
    /// </summary>
    /// <returns>A list of <see cref="IgdbGameDto"/></returns>
    public async Task<List<IgdbGameDto>?> GetGames()
    {
        var games = await _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id,name,cover,genres.name,platforms.name,artworks.image_id;;");

        if (games is null)
        {
            return null;
        }

        var gamesList = new List<IgdbGameDto>();
        foreach (var game in games)
        {
            string? artworkImageId = game.Artworks?.Values.FirstOrDefault()?.ImageId;
            string coverUrl = !string.IsNullOrEmpty(artworkImageId)
                ? IGDB.ImageHelper.GetImageUrl(imageId: artworkImageId, size: ImageSize.CoverSmall, retina: false)
                : string.Empty;
            
            gamesList.Add(new IgdbGameDto
            {
                Id = game.Id,
                Name = game.Name,
                Genres = game.Genres?.Values.Select(genre => genre.Name).ToList(),
                Platforms = game.Platforms?.Values.Select(platform => platform.Name).ToList(),
                CoverArtUrl = coverUrl
            });
        }

        return gamesList;
    }

    /// <summary>
    /// Get a game from the IGDB API based on a unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <see cref="Game"/></returns>
    public async Task<IgdbGameDto?> GetGame(int id)
    {
        var games = await _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: $"fields id,name,cover,genres.name,platforms.name,artworks.image_id; where id={id};");
        var game = games.FirstOrDefault();
        
        if (game is not null)
        {
            var artworkImageId = game.Artworks?.Values.FirstOrDefault()?.ImageId;
            string coverUrl = IGDB.ImageHelper.GetImageUrl(imageId: artworkImageId, size: ImageSize.CoverSmall, retina: false);
            
            return new IgdbGameDto
            {
                Id = game.Id,
                Name = game.Name,
                Genres = game.Genres?.Values.Select(genre => genre.Name).ToList(),
                Platforms = game.Platforms?.Values.Select(platform => platform.Name).ToList(),
                CoverArtUrl = coverUrl
            };
        }

        return null;
        // var games2 = await _client.QueryAsync<Game>(IGDB.IGDBClient.Endpoints.Games, query: "fields artworks.image_id; where id = 4;");

    }

    /// <summary>
    /// Returns a new instance of an IGDB Client.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="clientSecret"></param>
    /// <returns></returns>
    private IGDBClient CreateIGDBClient(string clientId, string clientSecret)
    {
        return new IGDBClient(
            clientId,
            clientSecret
        );
    }

}