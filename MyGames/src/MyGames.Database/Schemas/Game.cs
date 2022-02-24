using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyGames.Database.Schemas;

[BsonIgnoreExtraElements]
public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("igdbId")]
    public long? IgdbId { get; set; }
    
    [BsonElement("coverArtUrl")]
    public string? CoverArtUrl { get; set; }
    
    [BsonElement("status")]
    public string Status { get; set; }
}