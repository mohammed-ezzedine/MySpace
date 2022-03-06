using System.Runtime.Serialization;

namespace MySpace.Api.Domain.Exceptions;

public class TagNotFoundException : Exception
{
    public string Tag { get; private set; }

    public TagNotFoundException(string tag)
    {
        Tag = tag;
    }
}