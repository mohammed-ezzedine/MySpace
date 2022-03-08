namespace MySpace.Api.Application.Exceptions;

public class ArticleNotFoundException : Exception
{
    public string ArticleId { get; private set; }
   
    public ArticleNotFoundException(string id)
    {
        ArticleId = id;
    }

}