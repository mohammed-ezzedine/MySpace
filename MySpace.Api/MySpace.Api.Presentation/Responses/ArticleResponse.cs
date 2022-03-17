using System.Text.Json;
using System.Text.Json.Nodes;

namespace MySpace.Api.Presentation.Responses;

public class ArticleResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string EstimatedReadingTime { get; set; } = null!;

    public List<string>? Tags { get; set; }
    
    public string? ImageUrl { get; set; }

    public int ThumbsUp { get; set; }
    
    public int ThumbsDown { get; set; }

    public List<CommentResponse>? Comments { get; set; }
    
    public string Author { get; set; } = null!;

    public JsonNode Content { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime ModifiedDate { get; set; }
}