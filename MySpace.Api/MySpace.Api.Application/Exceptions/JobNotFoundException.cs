namespace MySpace.Api.Application.Exceptions;

public class JobNotFoundException: Exception
{
    public string JobId { get; private set; }
   
    public JobNotFoundException(string id) : base($"Job With ID {id} does not exist")
    {
        JobId = id;
    }

}