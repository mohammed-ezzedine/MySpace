namespace MySpace.Api.Domain.Models;

public class Article : ReactableDocument
{
    public ArticleId Id { get; set; }
    
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public int? EstimatedMinutesToRead { get; set; }

    public string? ImageUrl { get; set; }

    public List<Tag>? Tags { get; set; }


    public override void Update(Document document)
    {
        if (document.GetType() != typeof(Article))
        {
            throw new ArgumentException("Must pass an instance of type Article");
        }
        
        var article = (Article) document;
        UpdateArticle(article);
    }
    
    private void UpdateArticle(Article article)
    {
        base.Update(article);
        Title = article.Title ?? Title;
        Description = article.Description ?? Description;
        EstimatedMinutesToRead = article.EstimatedMinutesToRead ?? EstimatedMinutesToRead;
        Content = article.Content?? Content;
        Tags = article.Tags ?? Tags;
        ImageUrl = article.ImageUrl ?? ImageUrl;
        UpdateModifiedDate();
    }
}