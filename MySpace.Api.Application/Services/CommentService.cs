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

    public Article AddCommentToArticle(ArticleId articleId, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        article.AddComment(comment);
        return _articleService.UpdateArticle(articleId, article);
    }

    public Article AddReplyToComment(ArticleId articleId, CommentId parentCommentId, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        var parentComment = GetArticleComment(parentCommentId, article);
        parentComment.Comments ??= new List<Comment>();
        parentComment.Comments.Add(comment);
        return _articleService.UpdateArticle(articleId, article);
    }

    public Article EditComment(ArticleId articleId, CommentId id, Comment comment)
    {
        var article = _articleService.GetArticle(articleId);
        var originalComment = GetArticleComment(id, article);
        originalComment.Update(comment);
        return _articleService.UpdateArticle(articleId, article);
    }

    public void DeleteComment(ArticleId articleId, CommentId id)
    {
        var article = _articleService.GetArticle(articleId);
        GetArticleComment(id, article);
        article.RemoveComment(id);
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