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

    public MongoDbProjectRepository(PersistenceConfiguration configuration, IMapper mapper, ILogger<PersistenceFactory> logger)
    {
        _projectCollection = new PersistenceFactory(configuration, logger).GetCollection<ProjectEntity>("projects");
        _mapper = mapper;
    }

    public List<Project> GetProjects()
    {
        return _projectCollection.AsQueryable()
            .OrderByDescending(p => p.CreatedDate)
            .Select(_mapper.Map<Project>).ToList();
    }

    public Project GetProject(ProjectId id)
    {
        return _mapper.Map<Project>(_projectCollection.Find(MatchProjectId(id)).Single());
    }

    public Project AddProject(Project project)
    {
        project.Id = ProjectId.GetNewId();
        _projectCollection.InsertOne(_mapper.Map<ProjectEntity>(project));
        return project;
    }

    public Project EditProject(ProjectId id, Project project)
    {
        var originalProject = GetProject(id);
        originalProject.Update(project);
        _projectCollection.ReplaceOne(MatchProjectId(id), _mapper.Map<ProjectEntity>(originalProject));
        return originalProject;
    }

    public void DeleteProject(ProjectId id)
    {
        _projectCollection.DeleteOne(MatchProjectId(id));
    }

    public bool ProjectExists(ProjectId id)
    {
        return _projectCollection.CountDocuments(MatchProjectId(id)) > 0;
    }

    private static Expression<Func<ProjectEntity, bool>> MatchProjectId(ProjectId id)
    {
        return p => p.Id == id.Value;
    }
}