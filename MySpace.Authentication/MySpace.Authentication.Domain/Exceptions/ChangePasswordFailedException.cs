using Microsoft.AspNetCore.Identity;

namespace MySpace.Authentication.Domain.Exceptions;

public class ChangePasswordFailedException : Exception
{
    public IEnumerable<IdentityError> Errors { get; }

    public ChangePasswordFailedException(IEnumerable<IdentityError> errors) : base(GetMessage(errors))
    {
        Errors = errors;
    }

    private static string GetMessage(IEnumerable<IdentityError> errors)
    {
        return errors.Select(e => e.Description).Aggregate("", (curr, next) => curr + "\n\n" + next);
    }
}