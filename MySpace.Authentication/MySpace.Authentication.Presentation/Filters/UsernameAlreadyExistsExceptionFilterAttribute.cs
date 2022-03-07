using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Authentication.Domain.Exceptions;

namespace MySpace.Authentication.Presentation.Filters;

public class UsernameAlreadyExistsExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is UsernameAlreadyExistsException exception)
        {
            context.Result = new BadRequestObjectResult(exception.Message);
        }
    }
}