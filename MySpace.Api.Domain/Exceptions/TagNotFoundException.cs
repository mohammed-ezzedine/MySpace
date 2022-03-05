using System.Runtime.Serialization;

namespace MySpace.Api.Domain.Exceptions;

public class TagNotFoundException : Exception
{
    public TagNotFoundException()
    {
    }

    protected TagNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TagNotFoundException(string? message) : base(message)
    {
    }

    public TagNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}