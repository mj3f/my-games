using MyGames.Core.Dtos;

namespace MyGames.Core.Services;

/// <summary>
/// Wrapper service to call IGDB to get games data.
/// To get a users games, see UsersService.
/// </summary>
public sealed class GamesService
{
    private readonly TwitchLoginService _twitchLoginService;

    public GamesService(TwitchLoginService twitchLoginService) => _twitchLoginService = twitchLoginService;

    public async Task<GameDto> GetGame(string id)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders
        }    
    }
    
    
}