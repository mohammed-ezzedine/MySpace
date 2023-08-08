using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface ArticleRepository
{
    Page<Article> GetArticles(int pageIndex);
    Page<Article> GetArticlesByTag(Tag tag, int pageIndex);
    Page<Article> QueryArticles(string q, int pageIndex);
    Article GetArticle(int id);
    Article AddArticle(Article article);
    Article UpdateArticle(int id, Article article);
    void DeleteArticle(int id);
    bool ArticleExists(int id);
    bool ArticlesWithTagExist(string tag);
}