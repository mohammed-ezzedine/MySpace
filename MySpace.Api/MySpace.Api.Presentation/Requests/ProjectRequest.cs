using System.Text.Json.Nodes;

namespace MySpace.Api.Presentation.Requests;

public class ProjectRequest
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public string? Url { get; set; }
    
    public JsonNode? Content { get; set; }

    public DateTime? CreatedDate { get; set; }  
}