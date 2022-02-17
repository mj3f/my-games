namespace MyGames.Core.Dtos;

/// <summary>
/// DTO for representing a users game(s) in their collection, stored in MongoDB.
/// (Note: this DTO is distinct from IgdbGameDto, which contains the information of a game fetched from the IGDB API.)
/// </summary>
public sealed class GameDto // TODO: Convert to Record?
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The games unique identifier in the internet games database.
    /// </summary>
    public long? IgdbId { get; set; }
    
    /// <summary>
    /// The name of the game.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The games cover art as a url (if it exists).
    /// </summary>
    public string? CoverArtUrl { get; set; }

    /// <summary>
    /// Status of the game in the users library.
    /// </summary>
    public string GameStatus { get; set; }
    
    /// <summary>
    /// List of user-created notes for this game.
    /// </summary>
    public List<GameNoteDto> Notes { get; set; }
}