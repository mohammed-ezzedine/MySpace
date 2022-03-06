using MySpace.Api.Domain.Models;

namespace MySpace.Api.Domain.Persistence;

public interface ArticleRepository
{
    List<Article> GetArticles();
    List<Article> GetArticlesByTag(Tag tag);
    Article GetArticle(ArticleId id);
    Article AddArticle(Article article);
    Article UpdateArticle(ArticleId id, Article article);
    void DeleteArticle(ArticleId id);
    bool ArticleExists(ArticleId id);
}