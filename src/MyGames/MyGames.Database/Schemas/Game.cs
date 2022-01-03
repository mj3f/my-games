using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyGames.Database.Schemas;

public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("igdbId")]
    public string IgdbId { get; set; } = null!;
    
    [BsonElement("status")]
    public string Status { get; set; }
    
    [BsonRepresentation(BsonType.Array)]
    [BsonElement("notes")]
    public ICollection<GameNote> Notes { get; set; }
}