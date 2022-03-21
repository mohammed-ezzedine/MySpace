﻿using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class JobService : IJobService
{
    private readonly JobRepository _jobRepository;

    public JobService(JobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public List<Job> GetJobs()
    {
        return _jobRepository.GetJobs();
    }

    public Job? GetCurrentJob()
    {
        return _jobRepository.GetCurrentJob();
    }

    public Job GetJob(JobId id)
    {
        ThrowExceptionIfJobNotFound(id);
        return _jobRepository.GetJob(id);
    }

    public Job AddJob(Job job)
    {
        return _jobRepository.AddJob(job);
    }

    public Job EditJob(JobId id, Job job)
    {
        ThrowExceptionIfJobNotFound(id);
        return _jobRepository.EditJob(id, job);
    }

    public void DeleteJob(JobId id)
    {
        ThrowExceptionIfJobNotFound(id);
        _jobRepository.DeleteJob(id);
    }
    
    private void ThrowExceptionIfJobNotFound(JobId id)
    {
        if (!_jobRepository.JobExists(id))
        {
            throw new JobNotFoundException(id.ToString());
        }
    }
}