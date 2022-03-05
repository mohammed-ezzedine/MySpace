namespace MySpace.Api.Domain;

public abstract class Document
{
    public string Author { get; set; } = null!;

    public string? Content { get; set; }

    public DateOnly CreatedDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    
    public DateOnly ModifiedDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    protected void UpdateModifiedDate()
    {
        ModifiedDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public abstract void Update(Document document);
}