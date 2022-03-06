namespace MySpace.Api.Presentation.Requests;

public class ArticleRequest
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int EstimatedMinutesToRead { get; set; }

    public string? ImageUrl { get; set; }

    public List<TagRequest>? Tags { get; set; }
    
    public string Content { get; set; } = null!;
}