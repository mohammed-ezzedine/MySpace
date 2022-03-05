namespace MySpace.Api.Domain;

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

    public Comment? GetCommentOrDefault(CommentId id)
    {
        return Comments?.Find(c => c.Id.Equals(id));
    }

    public void DeleteComment(CommentId id)
    {
        Comments?.RemoveAll(c => c.Id.Equals(id));
    }
}