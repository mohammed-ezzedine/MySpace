namespace MySpace.Api.Domain.Models;

public readonly struct CommentId
{
    public Guid Value { get; }

    public CommentId(Guid id)
    {
        Value = id;
    }

    public static CommentId GetNewId()
    {
        return new CommentId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override bool Equals(object? obj)
    {
        var otherCommentId = (CommentId?) obj;
        return otherCommentId != null && Value == otherCommentId.Value.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(CommentId), Value);
    }
}