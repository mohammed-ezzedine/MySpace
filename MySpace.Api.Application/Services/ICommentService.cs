using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface ICommentService
{
    Article AddCommentToArticle(ArticleId articleId, Comment comment);
    Article AddReplyToComment(ArticleId articleId, CommentId parentCommentId, Comment comment);
    Article EditComment(ArticleId articleId, CommentId id, Comment comment);
    void DeleteComment(ArticleId articleId, CommentId id);
}