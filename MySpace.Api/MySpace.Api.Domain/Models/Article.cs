using System.Text.Json;
using System.Text.Json.Nodes;

namespace MySpace.Api.Domain.Models;

public class Article : ReactableDocument
{
    public int? Id { get; set; }
    
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public string? EstimatedReadingTime { get; set; }

    public string? ImageUrl { get; set; }

    public List<Tag>? Tags { get; set; }

    public new JsonNode? Content { get; set; }


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
        EstimatedReadingTime = article.EstimatedReadingTime ?? EstimatedReadingTime;
        Content = article.Content?? Content;
        Tags = article.Tags ?? Tags;
        ImageUrl = article.ImageUrl ?? ImageUrl;
        UpdateModifiedDate();
    }
}