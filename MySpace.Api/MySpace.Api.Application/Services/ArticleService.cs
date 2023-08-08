using F23.StringSimilarity;
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

    public Page<Article> GetArticles(int pageIndex)
    {
        return _articleRepository.GetArticles(pageIndex);
    }

    public Page<Article> GetArticlesByTag(Tag tag, int pageIndex)
    {
        return _articleRepository.GetArticlesByTag(tag, pageIndex);
    }

    public Page<Article> QueryArticles(string q, int pageIndex)
    {
        return _articleRepository.QueryArticles(q, pageIndex);
    }

    public Article GetArticle(int id)
    {
        ThrowExceptionIfArticleNotFound(id);
        return _articleRepository.GetArticle(id);
    }

    public Article AddArticle(Article article)
    {
        PersistArticleTags(article);
        return _articleRepository.AddArticle(article);
    }

    public Article UpdateArticle(int id, Article article)
    {
        ThrowExceptionIfArticleNotFound(id);
        PersistArticleTags(article);
        var originalArticle = GetArticle(id);
        DecrementOldTagsArticlesCount(originalArticle.Tags);
        
        originalArticle.Update(article);
        return _articleRepository.UpdateArticle(id, originalArticle);
    }

    public void DeleteArticle(int id)
    {
        ThrowExceptionIfArticleNotFound(id);

        var article = GetArticle(id);

        _articleRepository.DeleteArticle(id);
        
        DecrementOldTagsArticlesCount(article.Tags);
        DeleteUnusedTags(article.Tags);
    }

    private void DeleteUnusedTags(List<Tag>? tags)
    {
        var task = new Task(
            () => tags?.ForEach(t =>
            {
                if (!_articleRepository.ArticlesWithTagExist(t.Name)) _tagService.DeleteTag(t);
            })
        );
        
        task.Start();
    }

    private void DecrementOldTagsArticlesCount(List<Tag>? tags)
    {
        tags?.ForEach(t =>
        {
            if (t.Name == null) return;
            _tagService.DecrementTagArticlesCounter(t.Name);
        });
    }

    private void ThrowExceptionIfArticleNotFound(int id)
    {
        if (!_articleRepository.ArticleExists(id))
        {
            throw new ArticleNotFoundException(id.ToString());
        }
    }

    private void PersistArticleTags(Article article)
    {
        article.Tags?.ForEach(t =>
        {
            _tagService.AddTag(t);
            _tagService.IncrementTagArticlesCounter(t.Name);
        });
    }
}