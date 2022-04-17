using System.Text.Json.Nodes;

namespace MySpace.Api.Domain.Models;

public class Project
{
    public int? Id { get; set; }

    public string? Title { get; set; }
    
    public string? Description { get; set; }

    public string? Url { get; set; }
    
    public JsonNode? Content { get; set; }

    public DateTime? CreatedDate { get; set; }

    public void Update(Project other)
    {
        Title = other.Title ?? Title;
        Description = other.Description ?? Description;
        Url = other.Url ?? Url;
        Content = other.Content ?? Content;
        CreatedDate = other.CreatedDate ?? CreatedDate;
    }
}