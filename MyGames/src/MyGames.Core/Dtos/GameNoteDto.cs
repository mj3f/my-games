namespace MyGames.Core.Dtos;

public sealed record GameNoteDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; init; } = default!;
    
    /// <summary>
    /// When the note was created.
    /// </summary>
    public long CreatedAt { get; init; } = default!;
    
    /// <summary>
    /// Contents of the note.
    /// </summary>
    public string Content { get; init; } = default!;
}