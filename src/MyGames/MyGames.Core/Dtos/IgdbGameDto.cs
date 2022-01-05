namespace MyGames.Core.Dtos;

/// <summary>
/// Custom wrapper for game data fetched from IGDB.
/// This dto contains only the relevant data this project is interested in, nothing more.
/// </summary>
public sealed class IgdbGameDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long? Id { get; set; }
    
    /// <summary>
    /// The name of the game.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Id of games cover art.
    /// </summary>
    public string? CoverArtUrl { get; set; }
    
    /// <summary>
    /// List of genres the game belongs to.
    /// </summary>
    public List<string>? Genres { get; set; }

    /// <summary>
    /// List of platforms the game can be played on.
    /// </summary>
    public List<string>? Platforms { get; set; }
}