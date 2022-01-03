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
        _timer = new Timer(async _ => await Login(), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

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
        if (_twitchLoginService.TwitchLoginCredentials is not null)
        {
            Logger.Information("[BackgroundService] Logged into Twitch, token expires at " + _twitchLoginService.TwitchLoginCredentials.ExpiresIn);
        }
    }
}