namespace MyGames.Core.Dtos;

public class UserDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Users unique username that they created for themselves on signup.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// List of users games in their library.
    /// </summary>
    public List<GameDto> Games { get; set; }

}