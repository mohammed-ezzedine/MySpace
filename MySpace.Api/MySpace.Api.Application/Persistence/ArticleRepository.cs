using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface ArticleRepository
{
    List<Article> GetArticles();
    List<Article> GetArticlesByTag(Tag tag);
    List<Article> QueryArticles(String q);
    Article GetArticle(int id);
    Article AddArticle(Article article);
    Article UpdateArticle(int id, Article article);
    void DeleteArticle(int id);
    bool ArticleExists(int id);
}