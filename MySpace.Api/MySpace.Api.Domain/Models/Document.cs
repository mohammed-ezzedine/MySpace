namespace MySpace.Api.Domain.Models;

public abstract class Document
{
    public string Author { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime CreatedDate { get; set; }
    
    public DateTime ModifiedDate { get; set; }

    protected void UpdateModifiedDate()
    {
        ModifiedDate = DateTime.Now;
    }

    public abstract void Update(Document document);
}