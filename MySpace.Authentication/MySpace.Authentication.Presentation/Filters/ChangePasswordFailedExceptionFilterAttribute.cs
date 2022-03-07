using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Authentication.Domain.Exceptions;

namespace MySpace.Authentication.Presentation.Filters;

public class ChangePasswordFailedExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ChangePasswordFailedException exception)
        {
            context.Result = new BadRequestObjectResult(exception.Message);
        }
    }
}