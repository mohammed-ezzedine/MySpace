using MySpace.Api.Application.Exceptions;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class CommentService : ICommentService
{
    private readonly IArticleService _articleService;

    public CommentService(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public Comment GetComment(int articleId, string commentId)
    {
        var article = _articleService.GetArticle(articleId);
        var comment = GetArticleComment(commentId, article);
        return comment;
    }

    public Comment AddCommentToArticle(int articleId, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        article.AddComment(comment);
        _articleService.UpdateArticle(articleId, article);
        return comment;
    }

    public Comment AddReplyToComment(int articleId, string parentCommentId, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        var parentComment = GetArticleComment(parentCommentId, article);
        parentComment.Comments ??= new List<Comment>();
        parentComment.Comments.Add(comment);
        _articleService.UpdateArticle(articleId, article);
        return comment;
    }

    public Comment EditComment(int articleId, string id, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        var originalComment = GetArticleComment(id, article);
        originalComment.Update(comment);
        _articleService.UpdateArticle(articleId, article);
        return originalComment;
    }

    public void DeleteComment(int articleId, string id)
    {
        var article = _articleService.GetArticle(articleId);
        GetArticleComment(id, article);
        article.RemoveComment(id);
        _articleService.UpdateArticle(articleId, article);
    }

    private static Comment GetArticleComment(string commentId, Article article)
    {
        var comment = article.GetCommentOrDefault(commentId);
        if (comment == null)
        {
            throw new CommentNotFoundException(commentId.ToString());
        }

        return comment;
    }
}