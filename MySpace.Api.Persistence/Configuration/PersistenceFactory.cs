﻿using MongoDB.Driver;
using MySpace.Api.Domain.Configurations;

namespace MySpace.Api.Persistence.Configuration;

public class PersistenceFactory
{
    private readonly IMongoDatabase _database;
    
    public PersistenceFactory(PersistenceConfiguration configuration)
    {
        var settings = MongoClientSettings.FromConnectionString(configuration.ConnectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        
        _database = client.GetDatabase(configuration.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}