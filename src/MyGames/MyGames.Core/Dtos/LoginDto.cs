namespace MyGames.Core.Dtos;

/// <summary>
/// Record to capture the Unique identifier of the user, as well as their password.
/// </summary>
public record LoginDto(string Username, string Password)
{
}