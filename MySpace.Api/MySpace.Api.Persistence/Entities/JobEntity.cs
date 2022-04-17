using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MySpace.Api.Persistence.Entities;

public class JobEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    public string Employer { get; set; } = null!;

    public string EmployerImageUrl { get; set; } = null!;

    public string EmployerUrl { get; set; } = null!;

    public string Position { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public bool Active { get; set; }
}