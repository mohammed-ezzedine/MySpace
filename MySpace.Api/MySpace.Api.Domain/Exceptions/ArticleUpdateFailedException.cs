using System.Runtime.Serialization;

namespace MySpace.Api.Domain.Exceptions;

public class ArticleUpdateFailedException : Exception
{
    public ArticleUpdateFailedException()
    {
    }

    protected ArticleUpdateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ArticleUpdateFailedException(string? message) : base(message)
    {
    }

    public ArticleUpdateFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}