using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyGames.Database.Interfaces;

namespace MyGames.Database.Schemas;

[BsonIgnoreExtraElements]
public class User : IKeyedObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("username")] 
    public string Username { get; set; } = null!;

    [BsonElement("password")] 
    public string Password { get; set; } = null!;
    
    [BsonElement("salt")]
    public string Salt { get; set; } = null!;

    [BsonElement("games")] public ICollection<Game>? Games { get; set; }

    // Necessary for IDbRepository.
    public string Key => Username;

}