using Microsoft.AspNetCore.Identity;

namespace MySpace.Authentication.Domain.Exceptions;

public class RegisteringUserFailedException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; }

    public RegisteringUserFailedException(IEnumerable<IdentityError>? errors) : base("Failed to register the user. Reason(s):\n\n" + GetMessage(errors))
    {
        Errors = errors;
    }
    
    private static string GetMessage(IEnumerable<IdentityError> errors)
    {
        return errors.Select(e => e.Description).Aggregate("", (curr, next) => curr + "\n\n" + next);
    }
}