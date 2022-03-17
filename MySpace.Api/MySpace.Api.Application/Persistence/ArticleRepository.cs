using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface ArticleRepository
{
    List<Article> GetArticles();
    List<Article> GetArticlesByTag(Tag tag);
    List<Article> QueryArticles(String q);
    Article GetArticle(ArticleId id);
    Article AddArticle(Article article);
    Article UpdateArticle(ArticleId id, Article article);
    void DeleteArticle(ArticleId id);
    bool ArticleExists(ArticleId id);
}