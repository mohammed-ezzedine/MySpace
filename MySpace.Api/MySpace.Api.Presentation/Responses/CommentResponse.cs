namespace MySpace.Api.Presentation.Responses;

public class CommentResponse
{
    public Guid Id { get; set; }
    
    public int ThumbsUp { get; set; }
    
    public int ThumbsDown { get; set; }

    public List<CommentResponse>? Comments { get; set; }
    
    public string Author { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedDate { get; set;  }
    
    public DateTime ModifiedDate { get; set; }
}