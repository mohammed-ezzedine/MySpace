namespace MySpace.Api.Presentation.Responses;

public class JobResponse
{
    public string Id { get; set; }

    public string Employer { get; set; } = null!;
    
    public string EmployerUrl { get; set; } = null!;

    public string EmployerImageUrl { get; set; } = null!;
    
    public string Position { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public bool Active { get; set; }
}