using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MySpace.Api.Application.Configurations;

namespace MySpace.Api.Persistence.Configuration;

public class PersistenceFactory
{
    private readonly IMongoDatabase _database;
    private readonly ILogger _logger;
    
    public PersistenceFactory(PersistenceConfiguration configuration, ILogger<PersistenceFactory> logger)
    {
        _logger = logger;
        _logger.LogInformation($"Connecting to DB: '{configuration.ConnectionString}'");
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