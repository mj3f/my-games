namespace MyGames.Core.Dtos;

public class LoginDto
{
    /// <summary>
    /// Unique identifier of the user.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Users password.
    /// </summary>
    public string Password { get; set; }
}