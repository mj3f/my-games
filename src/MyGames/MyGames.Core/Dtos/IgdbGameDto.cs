namespace MyGames.Core.Dtos;

public sealed class IgdbGameDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Id of games cover art.
    /// </summary>
    public int Cover { get; set; }
    
    /// <summary>
    /// List of genres the game belongs to.
    /// </summary>
    public List<int> Genres { get; set; }
    
    /// <summary>
    /// The name of the game.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// List of platforms the game can be played on.
    /// </summary>
    public List<int> Platforms { get; set; }
}