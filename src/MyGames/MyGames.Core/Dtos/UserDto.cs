namespace MyGames.Core.Dtos;

public record UserDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// Users unique username that they created for themselves on signup.
    /// </summary>
    public string Username { get; init; }
    
    /// <summary>
    /// List of users games in their library.
    /// </summary>
    public List<GameDto> Games { get; init; }

}