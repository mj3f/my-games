using Microsoft.Extensions.Caching.Memory;
using MyGames.Core.Services;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MyGames.API.HostedServices;

public class BackgroundJobService : IHostedService
{
    private static readonly ILogger Logger = Log.ForContext<BackgroundService>();
    
    private readonly TwitchLoginService _twitchLoginService;

    private Timer _timer = null!;

    public BackgroundJobService(TwitchLoginService twitchLoginService) => _twitchLoginService = twitchLoginService;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.Information("[BackgroundService] Starting background tasks....");
        
        // Get twitch login token every 20 mintutes in order to be able to keep making valid API requests to IGDB.
        // _timer = new Timer(async _ => await Login(), null, TimeSpan.Zero, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.Information("[BackgroundService] Starting background tasks....");
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async Task Login()
    {
        await _twitchLoginService.Login();
    }
}