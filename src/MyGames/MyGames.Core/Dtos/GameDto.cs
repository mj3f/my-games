namespace MyGames.Core.Dtos;

public sealed class GameDto
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

public sealed class GameNoteDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// When the note was created.
    /// </summary>
    public long CreatedAt { get; set; }
    
    /// <summary>
    /// Contents of the note.
    /// </summary>
    public string Content { get; set; }
}