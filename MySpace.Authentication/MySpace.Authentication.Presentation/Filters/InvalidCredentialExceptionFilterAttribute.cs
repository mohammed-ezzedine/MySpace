using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Authentication.Domain.Exceptions;

namespace MySpace.Authentication.Presentation.Filters;

public class InvalidCredentialExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is InvalidCredentialException exception)
        {
            context.Result = new BadRequestObjectResult(exception.Message);
        }
    }
}