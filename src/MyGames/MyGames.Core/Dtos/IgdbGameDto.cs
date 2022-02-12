namespace MyGames.Core.Dtos;

/// <summary>
/// Custom wrapper for game data fetched from IGDB.
/// This dto contains only the relevant data this project is interested in, nothing more.
/// </summary>
public sealed record IgdbGameDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long? Id { get; init; }
    
    /// <summary>
    /// The name of the game.
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Id of games cover art.
    /// </summary>
    public string? CoverArtUrl { get; init; }
    
    /// <summary>
    /// List of genres the game belongs to.
    /// </summary>
    public List<string>? Genres { get; init; }

    /// <summary>
    /// List of platforms the game can be played on.
    /// </summary>
    public List<string>? Platforms { get; init; }
}