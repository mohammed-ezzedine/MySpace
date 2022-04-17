using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface IJobService
{
    List<Job> GetJobs();
    Job? GetCurrentJob();
    Job GetJob(int id);
    Job AddJob(Job job);
    Job EditJob(int id, Job job);
    void DeleteJob(int id);
}