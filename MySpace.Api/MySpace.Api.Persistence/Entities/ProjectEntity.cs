using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MySpace.Api.Persistence.Entities;

public class ProjectEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public string Url { get; set; } = null!;
    
    public BsonArray Content { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }
}