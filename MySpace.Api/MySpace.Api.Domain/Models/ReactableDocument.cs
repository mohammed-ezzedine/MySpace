namespace MySpace.Api.Domain.Models;

public abstract class ReactableDocument : Document
{
    public int ThumbsUp { get; private set; }
    
    public int ThumbsDown { get; private set; }

    public List<Comment>? Comments { get; set; }

    public void AddThumbUp()
    {
        ThumbsUp++;
    }

    public void AddThumbDown()
    {
        ThumbsDown++;
    }

    public void RemoveThumbUp()
    {
        ThumbsUp--;
    }

    public void RemoveThumbDown()
    {
        ThumbsDown--;
    }

    public void AddComment(Comment comment)
    {
        Comments ??= new List<Comment>();
        Comments.Add(comment);
    }

    public Comment? GetCommentOrDefault(string id)
    {
        return GetCommentRecursively(Comments, id).Result;
    }

    private async Task<Comment?> GetCommentRecursively(List<Comment>? comments, string id)
    {
        if (comments == null) return null;
        
        Comment? persistedComment = null;
        foreach (var comment in comments)
        {
            if (comment.Id.Equals(id))
            {
                return comment;
            }

            persistedComment = await Task.FromResult(GetCommentRecursively(comment.Comments, id).Result);
            if (persistedComment != null)
            {
                break;
            }
        }

        return persistedComment;
    }

    public void RemoveComment(string commentId)
    {
        RemoveCommentRecursively(Comments, commentId).Wait();
    }
    
    private async Task RemoveCommentRecursively(List<Comment>? comments, string id)
    {
        if (comments == null) return;

        comments.RemoveAll(c => c.Id.Equals(id));
        
        foreach (var comment in comments)
        {
            await Task.Run(() => RemoveCommentRecursively(comment.Comments, id));
        }
    }

    public void DeleteComment(string id)
    {
        Comments?.RemoveAll(c => c.Id.Equals(id));
    }

    public override void Update(Document document)
    {
        if (document is not ReactableDocument)
        {
            throw new ArgumentException("Must pass an instance of a ReactableDocument");
        }

        var reactableDocument = (ReactableDocument) document;
        Comments = reactableDocument.Comments ?? Comments;
    }
}