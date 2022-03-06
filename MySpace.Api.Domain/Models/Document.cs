namespace MySpace.Api.Domain.Models;

public abstract class Document
{
    public string Author { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime CreatedDate { get; } = DateTime.Now;
    
    public DateTime ModifiedDate { get; private set; } = DateTime.Now;

    protected void UpdateModifiedDate()
    {
        ModifiedDate = DateTime.Now;
    }

    public abstract void Update(Document document);
}