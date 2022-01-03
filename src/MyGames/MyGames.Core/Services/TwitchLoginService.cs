using System.Text.Json;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Models;
using Serilog;

namespace MyGames.Core.Services;

public sealed class TwitchLoginService
{
    public string ClientId { get; }
    
    private readonly string _clientSecret;
    private readonly HttpClient _httpClient;
    private static readonly ILogger Logger = Log.ForContext<TwitchLoginService>();

    private readonly ReaderWriterLockSlim _lock_creds_obj; // Allow multiple reading threads to get the value. See: https://codereview.stackexchange.com/a/254708
    private TwitchLoginCredentials? _credentials;

    public TwitchLoginCredentials? TwitchLoginCredentials
    {
        get
        {
            _lock_creds_obj.EnterReadLock();
            try
            {
                return _credentials;
            }
            finally
            {
                _lock_creds_obj.ExitReadLock();
            }
        }
    }
    
    public TwitchLoginService(IOptions<TwitchLoginSettings> loginSettings, HttpClient httpClient)
    {
        ClientId = loginSettings.Value.ClientId;
        _clientSecret = loginSettings.Value.ClientSecret;

        _httpClient = httpClient;

        _lock_creds_obj = new ReaderWriterLockSlim();
    }

    ~TwitchLoginService()
    {
        _lock_creds_obj.Dispose();
    }

    public async Task Login()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync(
                    $"https://id.twitch.tv/oauth2/token?client_id={ClientId}" +
                    $"&client_secret={_clientSecret}&grant_type=client_credentials", null);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                {
                    _lock_creds_obj.EnterWriteLock();
                    try
                    {
                        _credentials = JsonSerializer.Deserialize<TwitchLoginCredentials>(responseBody);
                    }
                    finally
                    {
                        _lock_creds_obj.ExitWriteLock();
                    }
                }
                
                Logger.Information("[TwitchLoginService] logged into twitch.");
        }
        catch (HttpRequestException ex)
        {
            Logger.Error("[TwitchLoginService] Login to twitch failed.");
        }
        
    }
}