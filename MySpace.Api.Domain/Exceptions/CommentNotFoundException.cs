using System.Runtime.Serialization;

namespace MySpace.Api.Domain.Exceptions;

public class CommentNotFoundException : Exception
{
    public CommentNotFoundException()
    {
    }

    protected CommentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CommentNotFoundException(string? message) : base(message)
    {
    }

    public CommentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}