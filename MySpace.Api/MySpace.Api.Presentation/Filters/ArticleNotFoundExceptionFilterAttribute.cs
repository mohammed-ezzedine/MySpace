using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySpace.Api.Application.Exceptions;

namespace MySpace.Api.Presentation.Filters;

public class ArticleNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ArticleNotFoundException exception)
        {
            context.Result = new NotFoundObjectResult($"Article of id '{exception.ArticleId}");
        }
    }
}