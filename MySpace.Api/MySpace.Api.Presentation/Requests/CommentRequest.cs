namespace MySpace.Api.Presentation.Requests;

public class CommentRequest
{
    public string Author { get; set; } = null!;

    public string Content { get; set; } = null!;
}