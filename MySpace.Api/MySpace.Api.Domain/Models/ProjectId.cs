namespace MySpace.Api.Domain.Models;

public readonly struct ProjectId
{
    public Guid Value { get; }

    public ProjectId(Guid id)
    {
        Value = id;
    }

    public static ProjectId GetNewId()
    {
        return new ProjectId(Guid.NewGuid());
    }

    public override bool Equals(object? obj)
    {
        var otherProjectId = (ProjectId?) obj;
        return otherProjectId != null && Value == otherProjectId.Value.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(ProjectId), Value);
    }
}