using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyGames.Database.Schemas;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("username")] 
    public string Username { get; set; } = null!;

    [BsonElement("password")] 
    public string Password { get; set; } = null!;
    
    [BsonElement("games")] 
    public ICollection<Game> Games { get; set; }


}