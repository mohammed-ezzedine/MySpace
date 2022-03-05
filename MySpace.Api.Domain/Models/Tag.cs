namespace MySpace.Api.Domain.Models;

public class Tag
{
    public string Name { get; private set; }

    public Tag(string name)
    {
        Name = name;
    }
    
    public void Update(Tag tag)
    {
        Name = tag.Name;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        var tag = (Tag?) obj;
        return tag != null && Name == tag.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(Tag), Name);
    }
}