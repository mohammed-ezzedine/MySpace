using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface IArticleService
{
    List<Article> GetArticles();
    List<Article> GetArticlesByTag(Tag tag);
    List<Article> QueryArticles(string q);
    Article GetArticle(int id);
    Article AddArticle(Article article);
    Article UpdateArticle(int id, Article article);
    void DeleteArticle(int id);
}