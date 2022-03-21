namespace MySpace.Api.Presentation.Requests;

public class JobRequest
{
    public string? Employer { get; set; }

    public string? EmployerUrl { get; set; }

    public string? EmployerImageUrl { get; set; }
    
    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public bool? Active { get; set; }
}