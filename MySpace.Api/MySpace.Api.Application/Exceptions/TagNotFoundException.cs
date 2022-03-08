namespace MySpace.Api.Application.Exceptions;

public class TagNotFoundException : Exception
{
    public string Tag { get; private set; }

    public TagNotFoundException(string tag)
    {
        Tag = tag;
    }
}