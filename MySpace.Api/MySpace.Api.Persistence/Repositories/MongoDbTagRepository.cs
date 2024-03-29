﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Persistence.Configuration;
using MySpace.Api.Persistence.Entities;
using Tag = MySpace.Api.Domain.Models.Tag;

namespace MySpace.Api.Persistence.Repositories;

public class MongoDbTagRepository : TagRepository
{
    private readonly IMongoCollection<TagEntity> _tagCollection;
    private readonly IMapper _mapper;

    public MongoDbTagRepository(PersistenceConfiguration configuration, IMapper mapper, ILogger<PersistenceFactory> logger)
    {
        _tagCollection = new PersistenceFactory(configuration, logger).GetCollection<TagEntity>("tags");
        _mapper = mapper;
    }

    public List<Tag> GetTags(int total = 20)
    {
        return _tagCollection.AsQueryable()
            .OrderByDescending(t => t.NumberOfArticles)
            .Take(total)
            .Select(_mapper.Map<Tag>)
            .ToList();
    }

    public Tag AddTag(Tag tag)
    {
        _tagCollection.InsertOne(_mapper.Map<TagEntity>(tag));
        return tag;
    }

    public void DeleteTag(Tag tag)
    {
        _tagCollection.DeleteOne(t => t.Name == tag.Name);
    }

    public void IncrementTagArticlesCounter(string tagName)
    {
        var filter = Builders<TagEntity>.Filter.Eq(t => t.Name, tagName);
        var update = Builders<TagEntity>.Update.Inc(t => t.NumberOfArticles, 1);

        _tagCollection.FindOneAndUpdate(filter, update);
    }

    public void DecrementTagArticlesCounter(string tagName)
    {
        var filter = Builders<TagEntity>.Filter.Eq(t => t.Name, tagName);
        var update = Builders<TagEntity>.Update.Inc(t => t.NumberOfArticles, -1);

        _tagCollection.FindOneAndUpdate(filter, update);
    }

    public bool TagExists(Tag tag)
    {
        return _tagCollection.CountDocuments(t => t.Name == tag.Name) > 0;
    }
}