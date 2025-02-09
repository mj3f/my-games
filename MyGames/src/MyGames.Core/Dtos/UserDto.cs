namespace MyGames.Core.Dtos;

public sealed record UserDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Users unique username that they created for themselves on signup.
    /// </summary>
    public string Username { get; init; } = string.Empty;
    
    /// <summary>
    /// List of users games in their library.
    /// </summary>
    public List<GameDto>? Games { get; init; }
}