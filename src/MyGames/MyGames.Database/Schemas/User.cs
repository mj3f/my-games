using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyGames.Database.Schemas;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("username")] public string Username { get; set; } = null!;

    [BsonElement("password")] public string Password { get; set; } = null!;
    
    [BsonRepresentation(BsonType.Array)]
    public ICollection<Game> Games { get; set; }


}