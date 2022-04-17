using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Persistence.Configuration;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Repositories;

public class MongoDbCounterRepository : CounterRepository
{
    private const string ARTICLE = "article";
    private const string PROJECT = "project";
    private const string COMMENT = "comment";
    private const string JOB = "job";

    private readonly IMongoCollection<CounterEntity> _counterCollection;

    public MongoDbCounterRepository(PersistenceConfiguration configuration, ILogger<PersistenceFactory> logger)
    {
        _counterCollection = new PersistenceFactory(configuration, logger).GetCollection<CounterEntity>("counters");
        InitializeCounters();
    }

    public int GenerateArticleId()
    {
        return GenerateNewId(ARTICLE);
    }

    public int GenerateCommentId()
    {
        return GenerateNewId(COMMENT);
    }

    public int GenerateProjectId()
    {
        return GenerateNewId(PROJECT);
    }

    public int GenerateJobId()
    {
        return GenerateNewId(JOB);
    }
    
    private int GenerateNewId(string counterId)
    {
        var filter = Builders<CounterEntity>.Filter.Eq(c => c.Id, counterId);
        var update = Builders<CounterEntity>.Update.Inc(c => c.LastUsedValue, 1);
        var counter = _counterCollection.FindOneAndUpdate(filter, update);

        return counter.LastUsedValue + 1;
    }

    private void InitializeCounters()
    {
        InitializeCounter(ARTICLE);
        InitializeCounter(PROJECT);
        InitializeCounter(COMMENT);
        InitializeCounter(JOB);
    }


    private void InitializeCounter(string counterId)
    {
        var filter = Builders<CounterEntity>.Filter.Eq(c => c.Id, counterId);
        var countExistingCounters = _counterCollection.CountDocuments(filter);
        if (countExistingCounters < 1)
        {
            _counterCollection.InsertOne(new CounterEntity {Id = counterId, LastUsedValue = 0});
        }
    }
}