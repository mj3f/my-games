namespace MyGames.Core.AppSettings;

/// <summary>
/// Mapping of string values for connecting to the MongoDB database, found in appSettings.json file of the API project.
/// </summary>
public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
}
