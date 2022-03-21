using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MySpace.Api.Application.Configurations;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;
using MySpace.Api.Persistence.Configuration;
using MySpace.Api.Persistence.Entities;

namespace MySpace.Api.Persistence.Repositories;

public class MongoDbJobRepository : JobRepository
{
    private readonly IMongoCollection<JobEntity> _jobCollection;
    private readonly IMapper _mapper;

    public MongoDbJobRepository(PersistenceConfiguration configuration, IMapper mapper, ILogger<PersistenceFactory> logger)
    {
        _jobCollection = new PersistenceFactory(configuration, logger).GetCollection<JobEntity>("jobs");
        _mapper = mapper;
    }

    public List<Job> GetJobs()
    {
        return _jobCollection.AsQueryable()
            .OrderByDescending(j => j.StartDate)
            .Select(_mapper.Map<Job>).ToList();
    }

    public Job GetCurrentJob()
    {
        return _mapper.Map<Job>(_jobCollection.AsQueryable().FirstOrDefault(j => j.Active));
    }

    public Job GetJob(JobId id)
    {
        return _mapper.Map<Job>(_jobCollection.AsQueryable().FirstOrDefault(MatchJobId(id)));
    }

    public Job AddJob(Job job)
    {
        job.Id = JobId.GetNewId();
        _jobCollection.InsertOne(_mapper.Map<JobEntity>(job));
        return job;
    }

    public Job EditJob(JobId id, Job job)
    {
        var originalJob = GetJob(id);
        originalJob.Update(job);
        _jobCollection.ReplaceOne(MatchJobId(id), _mapper.Map<JobEntity>(originalJob));
        return originalJob;
    }

    public void DeleteJob(JobId id)
    {
        _jobCollection.DeleteOne(MatchJobId(id));
    }

    public bool JobExists(JobId id)
    {
        return _jobCollection.CountDocuments(MatchJobId(id)) > 0;
    }
    
    private static Expression<Func<JobEntity, bool>> MatchJobId(JobId id)
    {
        return j => j.Id == id.Value;
    }
}