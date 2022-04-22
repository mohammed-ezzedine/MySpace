using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Configuration;
using MySpace.Api.Persistence.Entities;
using Tag = MySpace.Api.Domain.Models.Tag;

namespace MySpace.Api.Persistence.Repositories;

public class MongoDbArticleRepository : ArticleRepository
{
    private readonly IMongoCollection<ArticleEntity> _articleCollection;
    private readonly IMapper _mapper;
    private readonly CounterRepository _counterRepository;

    public MongoDbArticleRepository(PersistenceConfiguration configuration, IMapper mapper, ILogger<PersistenceFactory> logger, CounterRepository counterRepository)
    {
        _articleCollection = new PersistenceFactory(configuration, logger).GetCollection<ArticleEntity>("articles");
        _mapper = mapper;
        _counterRepository = counterRepository;
    }

    public List<Article> GetArticles()
    {
        return _articleCollection.AsQueryable()
            .AsEnumerable()
            .OrderByDescending(a => a.CreatedDate)
            .Select(_mapper.Map<Article>)
            .ToList();
    }

    public List<Article> GetArticlesByTag(Tag tag)
    {
        return  _articleCollection.AsQueryable()
            .Where(a => a.Tags != null && a.Tags.Any(t => t.Name == tag.Name))
            .OrderByDescending(a => a.CreatedDate)
            .AsEnumerable()
            .Select(_mapper.Map<Article>).ToList();
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

    private static Expression<Func<ArticleEntity, bool>> MatchArticleId(int id)
    {
        return a => a.Id == id;
    }
}