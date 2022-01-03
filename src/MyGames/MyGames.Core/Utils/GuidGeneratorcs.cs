namespace MyGames.Core.Utils;

public static class GuidGenerator
{
    public static string GenerateGuidForMongoDb() => Guid.NewGuid().ToString("N").Substring(0, 24);
}