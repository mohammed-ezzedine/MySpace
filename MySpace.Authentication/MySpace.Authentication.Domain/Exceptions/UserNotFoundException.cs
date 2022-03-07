namespace MySpace.Authentication.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public string Username { get; private set; }

    public UserNotFoundException(string username) : base($"User '{username}' does not exist.")
    {
        Username = username;
    }
}