using F23.StringSimilarity;
using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class ArticleService : IArticleService
{
    private const double SIMILARITY_SCORE_THRESHOLD = 0.6;
    private const double DISTANCE_SCORE_THRESHOLD = 0.4;

    private readonly ArticleRepository _articleRepository;
    private readonly ITagService _tagService;
    private readonly JaroWinkler _similarityScoreGenerator;

    public ArticleService(ArticleRepository articleRepository, ITagService tagService)
    {
        _articleRepository = articleRepository;
        _tagService = tagService;
        _similarityScoreGenerator = new JaroWinkler();
    }

    public List<Article> GetArticles()
    {
        return _articleRepository.GetArticles();
    }

    public List<Article> GetArticlesByTag(Tag tag)
    {
        return _articleRepository.GetArticlesByTag(tag);
    }

    public List<Article> QueryArticles(string q)
    {
        var articles = GetArticles();
        return articles
            .Where(a => ValuesAreSimilar(q, a.Title!) || ValuesAreSimilar(q, a.Description!))
            .ToList();
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
                if (!GetArticlesByTag(t).Any()) _tagService.DeleteTag(t);
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

    private bool ValuesAreSimilar(string value1, String value2)
    {
        var normalizedValue1 = value1.ToUpper();
        var normalizedValue2 = value2.ToUpper();
        return _similarityScoreGenerator.Similarity(normalizedValue1, normalizedValue2) > SIMILARITY_SCORE_THRESHOLD
            && _similarityScoreGenerator.Distance(normalizedValue1, normalizedValue2) < DISTANCE_SCORE_THRESHOLD;
    }
}