using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Configuration;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Repositories;

public class MongoDbProjectRepository : ProjectRepository
{
    private readonly IMongoCollection<ProjectEntity> _projectCollection;
    private readonly IMapper _mapper;
    private readonly CounterRepository _counterRepository;

    public MongoDbProjectRepository(PersistenceConfiguration configuration, IMapper mapper, ILogger<PersistenceFactory> logger, CounterRepository counterRepository)
    {
        _projectCollection = new PersistenceFactory(configuration, logger).GetCollection<ProjectEntity>("projects");
        _mapper = mapper;
        _counterRepository = counterRepository;
    }

    public List<Project> GetProjects()
    {
        return _projectCollection.AsQueryable()
            .OrderByDescending(p => p.CreatedDate)
            .Select(_mapper.Map<Project>).ToList();
    }

    public Project GetProject(int id)
    {
        return _mapper.Map<Project>(_projectCollection.Find(MatchProjectId(id)).Single());
    }

    public Project AddProject(Project project)
    {
        project.Id = _counterRepository.GenerateProjectId();
        _projectCollection.InsertOne(_mapper.Map<ProjectEntity>(project));
        return project;
    }

    public Project EditProject(int id, Project project)
    {
        var originalProject = GetProject(id);
        originalProject.Update(project);
        _projectCollection.ReplaceOne(MatchProjectId(id), _mapper.Map<ProjectEntity>(originalProject));
        return originalProject;
    }

    public void DeleteProject(int id)
    {
        _projectCollection.DeleteOne(MatchProjectId(id));
    }

    public bool ProjectExists(int id)
    {
        return _projectCollection.CountDocuments(MatchProjectId(id)) > 0;
    }

    private static Expression<Func<ProjectEntity, bool>> MatchProjectId(int id)
    {
        return p => p.Id == id;
    }
}