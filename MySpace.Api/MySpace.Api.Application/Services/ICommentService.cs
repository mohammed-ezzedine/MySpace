using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface ICommentService
{
    Comment GetComment(int articleId, string commentId);
    Comment AddCommentToArticle(int articleId, Comment comment);
    Comment AddReplyToComment(int articleId, string parentstring, Comment comment);
    Comment EditComment(int articleId, string id, Comment comment);
    void DeleteComment(int articleId, string id);
}