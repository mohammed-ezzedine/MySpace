using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Api.Application.Exceptions;

namespace MySpace.Api.Presentation.Filters;

public class JobNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is JobNotFoundException exception)
        {
            context.Result = new NotFoundObjectResult(exception.Message);
        }
    }
}