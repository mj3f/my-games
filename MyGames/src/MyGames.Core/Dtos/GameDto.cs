namespace MyGames.Core.Dtos;

/// <summary>
/// DTO for representing a users game(s) in their collection, stored in MongoDB.
/// (Note: this DTO is distinct from IgdbGameDto, which contains the information of a game fetched from the IGDB API.)
/// </summary>
public sealed record GameDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// The games unique identifier in the internet games database.
    /// </summary>
    public long? IgdbId { get; init; } = default!;
    
    /// <summary>
    /// The name of the game.
    /// </summary>
    public string Name { get; init; } = default!;
    
    /// <summary>
    /// The games cover art as a url (if it exists).
    /// </summary>
    public string? CoverArtUrl { get; init; } = default!;

    /// <summary>
    /// Status of the game in the users library.
    /// </summary>
    public string GameStatus { get; init; } = default!;
    
    /// <summary>
    /// List of user-created notes for this game.
    /// </summary>
    public List<GameNoteDto> Notes { get; init; } = default!;
}