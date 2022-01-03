using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyGames.Database.Schemas;

public class GameNote
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("created_at")]
    public long CreatedAt { get; set; }
    
    [BsonElement("content")]
    public string Content { get; set; }
}