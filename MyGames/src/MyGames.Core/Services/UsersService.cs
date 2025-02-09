using MongoDB.Driver;
using MyGames.Database.Schemas;
using Microsoft.Extensions.Options;
using MyGames.Core.AppSettings;
using MyGames.Core.Dtos;
using MyGames.Core.Enums;
using MyGames.Core.Repositories;
using MyGames.Core.Services.Interfaces;
using MyGames.Core.Utils;
using Serilog;

namespace MyGames.Core.Services;

/// <summary>
/// Service to fetch user data from the myGames API.
/// </summary>
public sealed class UsersService : IUsersService
{
    private static readonly ILogger Logger = Log.ForContext<UsersService>();

    private readonly IUserRepository _repository;

    // dbSettings values populated from the appSettings.json file in the myGames.API project.
    public UsersService(IUserRepository repository) => _repository = repository;

    /// <summary>
    /// Returns a list of users in the myGames Database.
    /// This method should not be callable by a regular user.
    /// </summary>
    /// <returns>A list of <see cref="UserDto"/></returns>
    public async Task<List<UserDto>> GetUsers()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(ConvertUserToUserDto).ToList();
    }

    /// <summary>
    /// Gets a user by the unique identifier (user provided username) provided.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>A <see cref="UserDto"/></returns>
    public async Task<UserDto?> GetByUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            Logger.Error("[USERS SERVICE] No username provided.");
            return null;
        }
        
        var user = await _repository.GetByIdAsync(username);

        return user is not null ? ConvertUserToUserDto(user) : null;
    }

    /// <summary>
    /// Adds a game to a users library.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="gameToAdd"></param>
    /// <exception cref="Exception">Throws an exception if the game could not be added to the db.</exception>
    public async Task<GameDto?> AddGameToUsersLibrary(string username, IgdbGameDto gameToAdd)
    {
        if (string.IsNullOrEmpty(username))
        {
            Logger.Error("[USERS SERVICE] No username provided when trying to add a game.");
            return null;
        }
        
        // Convert the igdb game to a Game (mongodb schema).
        var game = new Game
        {
            Id = GuidGenerator.GenerateGuidForMongoDb(),
            IgdbId = gameToAdd.Id,
            Name = gameToAdd.Name,
            CoverArtUrl = gameToAdd.CoverArtUrl,
            Status = GameStatus.Backlog
        };

        try
        {
            await _repository.AddGameToUsersLibraryAsync(username, game);
            return new GameDto // This is ugly, is there a better way??
            {
                Id = game.Id,
                IgdbId = game.IgdbId,
                Name = game.Name,
                CoverArtUrl = game.CoverArtUrl,
                Notes = new List<GameNoteDto>(),
                GameStatus = game.Status
            };
        }
        catch (Exception ex)
        {
            Logger.Error("[UsersService] Error occurred whilst trying to add a game to a users library. " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Removes a game from the users library if it exists.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="gameId"></param>
    /// <exception cref="Exception">Throws an exception if the game could not be removed from the db.</exception>
    public async Task<bool> RemoveGameFromUsersLibrary(string username, string gameId)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(gameId))
        {
            Logger.Error("[USERS SERVICE] No username or game ID provided whilst trying to remove a game from users library.");
            return false;
        }
        
        try
        {
            await _repository.RemoveGameFromUsersLibraryAsync(username, gameId);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error("[UsersService] Error occurred whilst trying to remove a game from a users library. " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Updates a game in the user library, i.e. changing its status in the library.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="game"></param>
    /// <exception cref="Exception">Throws an exception if the game could not be updated in the db.</exception>
    public async Task UpdateGameInUsersLibrary(string username, GameDto game)
    {
        if (string.IsNullOrEmpty(username))
        {
            Logger.Error("[USERS SERVICE] No username provided whilst trying to update game.");
            return;
        }
        
        var gameDb = new Game
        {
            Id = game.Id,
            IgdbId = game.IgdbId,
            Name = game.Name,
            CoverArtUrl = game.CoverArtUrl,
            Status = game.GameStatus
        };

        try
        {
            await _repository.UpdateGameInUsersLibraryAsync(username, gameDb);
        }
        catch (Exception ex)
        {
            Logger.Error("[UsersService] Error occurred whilst trying update the game. " + ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Converts a user schema returned from the mongoDB users collection into a User Dto.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A <see cref="UserDto"/></returns>
    private UserDto ConvertUserToUserDto(User user) => new()
    {
        Id = user.Id!,
        Username = user.Username,
        Games = user.Games?.Select(g => new GameDto
        {
            Id = g.Id!,
            Name = g.Name,
            GameStatus = g.Status,
            IgdbId = g.IgdbId,
            CoverArtUrl = g.CoverArtUrl,
        }).ToList()
    };
}