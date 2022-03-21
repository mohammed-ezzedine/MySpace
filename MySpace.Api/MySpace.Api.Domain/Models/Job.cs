namespace MySpace.Api.Domain.Models;

public class Job
{
    public JobId Id { get; set; }

    public string? Employer { get; set; }
    
    public string? EmployerImageUrl { get; set; }
    
    public string? EmployerUrl { get; set; }

    public string? Position { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Active { get; set; }

    public void Update(Job other)
    {
        Employer = other.Employer ?? Employer;
        EmployerUrl = other.EmployerUrl ?? EmployerUrl;
        EmployerImageUrl = other.EmployerImageUrl ?? EmployerImageUrl;
        Position = other.Position ?? Position;
        StartDate = other.StartDate ?? StartDate;
        EndDate = other.EndDate ?? EndDate;
        Active = other.Active ?? Active;
    }
}