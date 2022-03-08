using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Api.Application.Exceptions;

namespace MySpace.Api.Presentation.Filters;

public class TagNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is TagNotFoundException exception)
        {
            context.Result = new NotFoundObjectResult($"Tag '{exception.Tag}' was not found.");
        }
    }
}