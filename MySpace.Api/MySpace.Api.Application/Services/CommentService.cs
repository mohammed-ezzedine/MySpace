using MySpace.Api.Domain.Exceptions;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class CommentService : ICommentService
{
    private readonly IArticleService _articleService;

    public CommentService(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public Comment GetComment(ArticleId articleId, CommentId commentId)
    {
        var article = _articleService.GetArticle(articleId);
        var comment = GetArticleComment(commentId, article);
        return comment;
    }

    public Comment AddCommentToArticle(ArticleId articleId, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        comment.Id = CommentId.GetNewId();
        article.AddComment(comment);
        _articleService.UpdateArticle(articleId, article);
        return comment;
    }

    public Comment AddReplyToComment(ArticleId articleId, CommentId parentCommentId, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        comment.Id = CommentId.GetNewId();
        var parentComment = GetArticleComment(parentCommentId, article);
        parentComment.Comments ??= new List<Comment>();
        parentComment.Comments.Add(comment);
        _articleService.UpdateArticle(articleId, article);
        return comment;
    }

    public Comment EditComment(ArticleId articleId, CommentId id, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        var originalComment = GetArticleComment(id, article);
        originalComment.Update(comment);
        _articleService.UpdateArticle(articleId, article);
        return originalComment;
    }

    public void DeleteComment(ArticleId articleId, CommentId id)
    {
        var article = _articleService.GetArticle(articleId);
        GetArticleComment(id, article);
        article.RemoveComment(id);
        _articleService.UpdateArticle(articleId, article);
    }

    private static Comment GetArticleComment(CommentId commentId, Article article)
    {
        var comment = article.GetCommentOrDefault(commentId);
        if (comment == null)
        {
            throw new CommentNotFoundException(commentId.ToString());
        }

        return comment;
    }
}