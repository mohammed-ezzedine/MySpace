using System.Runtime.Serialization;

namespace MySpace.Api.Domain.Exceptions;

public class CommentNotFoundException : Exception
{
    public string CommentId { get; private set; }

    public CommentNotFoundException(string commentId)
    {
        CommentId = commentId;
    }
}