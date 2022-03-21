using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface JobRepository
{
    List<Job> GetJobs();
    Job GetCurrentJob();
    Job GetJob(JobId id);
    Job AddJob(Job job);
    Job EditJob(JobId id, Job job);
    void DeleteJob(JobId id);
    bool JobExists(JobId id);
}