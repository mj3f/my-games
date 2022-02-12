namespace MyGames.Core.Dtos;

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