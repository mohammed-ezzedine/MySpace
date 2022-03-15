using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class ArticleService : IArticleService
{
    private readonly ArticleRepository _articleRepository;
    private readonly ITagService _tagService;

    public ArticleService(ArticleRepository articleRepository, ITagService tagService)
    {
        _articleRepository = articleRepository;
        _tagService = tagService;
    }

    public List<Article> GetArticles()
    {
        return _articleRepository.GetArticles().OrderByDescending(a => a.CreatedDate).ToList();
    }

    public List<Article> GetArticlesByTag(Tag tag)
    {
        return _articleRepository.GetArticlesByTag(tag);
    }

    public Article GetArticle(ArticleId id)
    {
        ThrowExceptionIfArticleNotFound(id);
        return _articleRepository.GetArticle(id);
    }

    public Article AddArticle(Article article)
    {
        PersistArticleTags(article);
        article.Id = ArticleId.GetNewId();
        return _articleRepository.AddArticle(article);
    }

    public Article UpdateArticle(ArticleId id, Article article)
    {
        ThrowExceptionIfArticleNotFound(id);
        PersistArticleTags(article);
        article.Update(article);
        return _articleRepository.UpdateArticle(id, article);
    }

    public void DeleteArticle(ArticleId id)
    {
        ThrowExceptionIfArticleNotFound(id);
        _articleRepository.DeleteArticle(id);
    }

    private void ThrowExceptionIfArticleNotFound(ArticleId id)
    {
        if (!_articleRepository.ArticleExists(id))
        {
            throw new ArticleNotFoundException(id.ToString());
        }
    }

    private void PersistArticleTags(Article article)
    {
        article.Tags?.ForEach(t => _tagService.AddTag(t));
    }
}