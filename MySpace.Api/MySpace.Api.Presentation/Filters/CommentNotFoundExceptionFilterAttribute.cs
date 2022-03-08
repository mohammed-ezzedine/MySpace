using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Api.Application.Exceptions;

namespace MySpace.Api.Presentation.Filters;

public class CommentNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is CommentNotFoundException exception)
        {
            context.Result = new NotFoundObjectResult($"Comment of ID '{exception.CommentId}' was not found.");
        }
    }
}