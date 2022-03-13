using System.Text.Json;
using System.Text.Json.Nodes;

namespace MySpace.Api.Presentation.Requests;

public class ArticleRequest
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int EstimatedMinutesToRead { get; set; }

    public string? ImageUrl { get; set; }

    public List<string>? Tags { get; set; }
    
    public JsonNode Content { get; set; }
}