using System.Text.Json.Nodes;

namespace MySpace.Api.Presentation.Responses;

public class ProjectResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public string Url { get; set; } = null!;
    
    public JsonNode Content { get; set; } = null!;

    public DateTime CreatedDate { get; set; }   
}