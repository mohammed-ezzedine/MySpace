namespace MySpace.Api.Domain.Models;

public readonly struct ArticleId
{
    public Guid Value { get; }

    public ArticleId(Guid id)
    {
        Value = id;
    }

    public static ArticleId GetNewId()
    {
        return new ArticleId(Guid.NewGuid());
    }

    public override bool Equals(object? obj)
    {
        var otherArticleId = (ArticleId?) obj;
        return otherArticleId != null && Value == otherArticleId.Value.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(ArticleId), Value);
    }
}