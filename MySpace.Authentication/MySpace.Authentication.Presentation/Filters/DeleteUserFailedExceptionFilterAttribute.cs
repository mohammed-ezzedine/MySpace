using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Authentication.Domain.Exceptions;

namespace MySpace.Authentication.Presentation.Filters;

public class DeleteUserFailedExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is DeleteUserFailedException exception)
        {
            context.Result = new BadRequestObjectResult(exception.Message);
        }
    }
}