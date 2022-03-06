using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface ICommentService
{
    Comment GetComment(ArticleId articleId, CommentId commentId);
    Comment AddCommentToArticle(ArticleId articleId, Comment comment);
    Comment AddReplyToComment(ArticleId articleId, CommentId parentCommentId, Comment comment);
    Comment EditComment(ArticleId articleId, CommentId id, Comment comment);
    void DeleteComment(ArticleId articleId, CommentId id);
}