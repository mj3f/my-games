namespace MyGames.Core.Utils;

/// <summary>
/// Utility class with a static method to generate a GUID string that is compliant with MongoDb ObjectId restrictions.
/// </summary>
public static class GuidGenerator
{
    /// <summary>
    /// Creates a GUID string which is compliant with MongoDB objectId rules.
    /// The string generated contains no hypens (-), and is of length 24 characters.
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public static string GenerateGuidForMongoDb() => Guid.NewGuid().ToString("N").Substring(0, 24);
}