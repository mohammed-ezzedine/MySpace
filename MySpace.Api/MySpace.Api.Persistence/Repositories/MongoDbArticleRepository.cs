using System.Linq.Expressions;
using AutoMapper;
using F23.StringSimilarity;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Configuration;
using MySpace.Api.Persistence.Entities;
using Tag = MySpace.Api.Domain.Models.Tag;

namespace MySpace.Api.Persistence.Repositories;

public class MongoDbArticleRepository : ArticleRepository
{
    private const double SimilarityScoreThreshold = 0.5;
    private const double DistanceScoreThreshold = 0.5;
    
    private const int PageSize = 5;
    private readonly IMongoCollection<ArticleEntity> _articleCollection;
    private readonly IMapper _mapper;
    private readonly CounterRepository _counterRepository;
    private readonly JaroWinkler _similarityScoreGenerator;

    public MongoDbArticleRepository(PersistenceConfiguration configuration, IMapper mapper, ILogger<PersistenceFactory> logger, CounterRepository counterRepository)
    {
        _articleCollection = new PersistenceFactory(configuration, logger).GetCollection<ArticleEntity>("articles");
        _mapper = mapper;
        _counterRepository = counterRepository;
        _similarityScoreGenerator = new JaroWinkler();
    }

    public Page<Article> GetArticles(int pageIndex)
    {
        var articles = _articleCollection.AsQueryable()
            .AsEnumerable()
            .OrderByDescending(a => a.CreatedDate)
            .ToList();

        var paginatedArticles = articles.AsEnumerable()
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .Select(_mapper.Map<Article>)
            .ToList();

        var totalNumberOfPages = (int) Math.Ceiling(articles.Count / (double) PageSize);

        return new Page<Article>
        {
            Items = paginatedArticles,
            PageNumber = pageIndex,
            TotalNumberOfPages = totalNumberOfPages
        };
    }

    public Page<Article> GetArticlesByTag(Tag tag, int pageIndex)
    {
        var articles = _articleCollection.AsQueryable()
            .Where(a => a.Tags != null && a.Tags.Any(t => t.Name == tag.Name))
            .OrderByDescending(a => a.CreatedDate)
            .ToList();

        var paginatedArticles = articles
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .AsEnumerable()
            .Select(_mapper.Map<Article>)
            .ToList();

        var totalNumberOfPages = (int) Math.Ceiling(articles.Count / (double) PageSize);

        return new Page<Article>
        {
            Items = paginatedArticles,
            PageNumber = pageIndex,
            TotalNumberOfPages = totalNumberOfPages
        };
    }

    public Page<Article> QueryArticles(string q, int pageIndex)
    {
        var articles = _articleCollection.AsQueryable()
            .OrderByDescending(a => a.CreatedDate)
            .ToList();

        articles = articles.Where(a => ValuesAreSimilar(q, a.Title!) || ValuesAreSimilar(q, a.Description!))
            .ToList();

        var paginatedArticles = articles
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .AsEnumerable()
            .Select(_mapper.Map<Article>).ToList();
        
        var totalNumberOfPages = (int) Math.Ceiling(articles.Count / (double) PageSize);

        return new Page<Article>
        {
            Items = paginatedArticles,
            PageNumber = pageIndex,
            TotalNumberOfPages = totalNumberOfPages
        };
    }

    public Article GetArticle(int id)
    {
        return _mapper.Map<Article>(_articleCollection.Find(MatchArticleId(id)).First());
    }

    public Article AddArticle(Article article)
    {
        article.Id = _counterRepository.GenerateArticleId();
        article.CreatedDate = DateTime.Now;
        article.ModifiedDate = DateTime.Now;
        var articleEntity = _mapper.Map<ArticleEntity>(article);
        _articleCollection.InsertOne(articleEntity);
        return article;
    }

    public Article UpdateArticle(int id, Article article)
    {
        article.Id = id;
        var result = _articleCollection.ReplaceOne(MatchArticleId(id), _mapper.Map<ArticleEntity>(article));
        if (!result.IsAcknowledged)
        {
            throw new ArticleUpdateFailedException();
        }

        return article;
    }

    public void DeleteArticle(int id)
    {
        _articleCollection.DeleteOne(MatchArticleId(id));
    }

    public bool ArticleExists(int id)
    {
        return _articleCollection.CountDocuments(MatchArticleId(id)) > 0;
    }

    public bool ArticlesWithTagExist(string tag)
    {
        return _articleCollection.AsQueryable()
            .Any(a => a.Tags != null && a.Tags.Any(t => t.Name == tag));
    }

    private static Expression<Func<ArticleEntity, bool>> MatchArticleId(int id)
    {
        return a => a.Id == id;
    }

    private bool ValuesAreSimilar(string value1, string value2)
    {
        var normalizedValue1 = value1.ToUpper();
        var normalizedValue2 = value2.ToUpper();
        return _similarityScoreGenerator.Similarity(normalizedValue1, normalizedValue2) > SimilarityScoreThreshold
               && _similarityScoreGenerator.Distance(normalizedValue1, normalizedValue2) < DistanceScoreThreshold;
    }
}