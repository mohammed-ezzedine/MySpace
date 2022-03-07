namespace MySpace.Authentication.Domain.Exceptions;

public class InvalidCredentialException : Exception
{
    public InvalidCredentialException() : base("Invalid credentials.")
    {
    }
}