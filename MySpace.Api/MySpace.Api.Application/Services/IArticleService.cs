using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface IArticleService
{
    Page<Article> GetArticles(int pageIndex);
    Page<Article> GetArticlesByTag(Tag tag, int pageIndex);
    Page<Article> QueryArticles(string q, int pageIndex);
    Article GetArticle(int id);
    Article AddArticle(Article article);
    Article UpdateArticle(int id, Article article);
    void DeleteArticle(int id);
}