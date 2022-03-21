using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Persistence.Entities;

public class ArticleEntity
{
    [BsonId]
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string EstimatedReadingTime { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public List<Tag>? Tags { get; set; }
    
    public int ThumbsUp { get; set; }
    
    public int ThumbsDown { get; set; }

    public List<CommentEntity>? Comments { get; set; }
    
    public string Author { get; set; } = null!;

    public BsonArray Content { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime ModifiedDate { get; set; }
}