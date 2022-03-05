namespace MySpace.Api.Domain.Models;

public class CommentId : ValueType
{
    public Guid Value { get; }

    private CommentId(Guid id)
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
        return otherCommentId != null && Value.Equals(otherCommentId.Value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(CommentId), Value);
    }
}