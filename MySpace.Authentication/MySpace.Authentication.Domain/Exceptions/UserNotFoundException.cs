namespace MySpace.Authentication.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public string Id { get; private set; }

    public UserNotFoundException(string id)
    {
        Id = id;
    }
}