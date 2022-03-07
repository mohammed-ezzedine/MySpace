using Microsoft.AspNetCore.Identity;

namespace MySpace.Authentication.Domain.Exceptions;

public class DeleteUserFailedException : Exception
{
    public string? Reason { get; }
    
    public IEnumerable<IdentityError>? Errors { get; }

    public DeleteUserFailedException(string reason) : base($"Failed to delete user. Reason: {reason}")
    {
        Reason = reason;
    }

    public DeleteUserFailedException(IEnumerable<IdentityError> errors) : base($"Failed to delete user. Reason(s):\n" + GetMessage(errors))
    {
        Errors = errors;
    }

    private static string GetMessage(IEnumerable<IdentityError> errors)
    {
        return errors.Select(e => e.Description).Aggregate("", (curr, next) => curr + "\n\n" + next);
    }
}