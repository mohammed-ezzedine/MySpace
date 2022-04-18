using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MySpace.Api.Persistence.Entities;

public class TagEntity
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int NumberOfArticles { get; set; }
}