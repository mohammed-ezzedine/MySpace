namespace MySpace.Api.Application.Exceptions;

public class ProjectNotFoundException : Exception
{
    public string ProjectId { get; private set; }
   
    public ProjectNotFoundException(string id) : base($"Job With ID {id} does not exist")
    {
        ProjectId = id;
    }
}