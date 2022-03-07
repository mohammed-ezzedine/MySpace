namespace MySpace.Authentication.Domain.Exceptions;

public class UsernameAlreadyExistsException : Exception
{
    public string Username { get; private set; }

    public UsernameAlreadyExistsException(string username) : base($"Username '{username}' is already taken.")
    {
        Username = username;
    }
}