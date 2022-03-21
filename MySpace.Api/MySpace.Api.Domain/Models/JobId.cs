namespace MySpace.Api.Domain.Models;

public readonly struct JobId
{
    public Guid Value { get; }

    public JobId(Guid id)
    {
        Value = id;
    }

    public static JobId GetNewId()
    {
        return new JobId(Guid.NewGuid());
    }

    public override bool Equals(object? obj)
    {
        var otherJonId = (JobId?) obj;
        return otherJonId != null && Value == otherJonId.Value.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(JobId), Value);
    }
}