using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MySpace.Api.Persistence.Entities;

public class CounterEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = null!;

    public int LastUsedValue { get; set; }
}