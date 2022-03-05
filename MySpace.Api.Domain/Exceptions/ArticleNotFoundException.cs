using System.Runtime.Serialization;

namespace MySpace.Api.Domain.Exceptions;

public class ArticleNotFoundException : Exception
{
    public ArticleNotFoundException()
    {
    }

    protected ArticleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ArticleNotFoundException(string? message) : base(message)
    {
    }

    public ArticleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}