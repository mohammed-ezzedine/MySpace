namespace MySpace.Api.Application.Exceptions;

public class CommentNotFoundException : Exception
{
    public string CommentId { get; private set; }

    public CommentNotFoundException(string commentId)
    {
        CommentId = commentId;
    }
}