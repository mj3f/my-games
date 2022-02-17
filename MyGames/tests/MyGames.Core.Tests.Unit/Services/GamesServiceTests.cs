using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Services;
using MyGames.Core.Services.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace MyGames.Core.Tests.Unit.Services;
public class GamesServiceTests
{
    private readonly IGamesService _sut;
    private readonly IOptions<TwitchLoginSettings> _loginSettings = Options.Create(new TwitchLoginSettings());
    private readonly IGDBClient _client = Substitute.For<IGDBClient>();

    public GamesServiceTests()
    {
        _loginSettings.Value.ClientId = "test_client_id";
        _loginSettings.Value.ClientSecret = "test_client_secret";
        _sut = new GamesService(_loginSettings, _client);
    }

    [Fact(Skip = "Can't mock the IGDBClient, don't think this service is unit testable tbh, integration tests more " +
                 "appropriate.")]
    public async Task GetGames_ShouldReturnNull_WhenNoGamesFound()
    {
        // Cannot unit test case where games are found, should be an integration test with real data instead.
        
        // Arrange
        _client.QueryAsync<Game>(IGDBClient.Endpoints.Games, "").ReturnsNull();

        // Act
        var games = await _sut.GetGames();
        
        // Assert
        Assert.Null(games);
    }
}