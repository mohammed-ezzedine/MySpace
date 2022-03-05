
namespace MySpace.Api.Domain;

public class Comment : ReactableDocument
{
    public CommentId Id { get; set; } = null!;
    
    public override void Update(Document document)
    {
        if (document.GetType() != typeof(Comment))
        {
            throw new ArgumentException("Must pass an instance of type Comment");
        }

        var comment = (Comment) document;
        UpdateComponent(comment);
    }

    public void UpdateComponent(Comment comment)
    {
        Content = comment.Content ?? Content;
        UpdateModifiedDate();
    }
}