using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Filters;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;

namespace MySpace.Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IMapper _mapper;

    public JobController(IJobService jobService, IMapper mapper)
    {
        _jobService = jobService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<JobResponse>))]
    public ActionResult<List<JobResponse>> GetJobs()
    {
        var jobs = _jobService.GetJobs().Select(_mapper.Map<JobResponse>).ToList();
        return Ok(jobs);
    }
    
    [HttpGet("current")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(JobResponse))]
    public ActionResult<List<JobResponse>> GetCurrentJob()
    {
        var job = _mapper.Map<JobResponse>(_jobService.GetCurrentJob());
        return Ok(job);
    }

    [HttpGet("{id:int}")]
    [JobNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(JobResponse))]
    public ActionResult<List<JobResponse>> GetJob(int id)
    {
        var job = _mapper.Map<JobResponse>(_jobService.GetJob(id));
        return Ok(job);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(JobResponse))]
    public ActionResult<List<JobResponse>> AddJob(JobRequest request)
    {
        var persistedJob = _jobService.AddJob(_mapper.Map<Job>(request));
        var job = _mapper.Map<JobResponse>(persistedJob);
        return Created(job.Id.ToString(), job);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    [JobNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<JobResponse>))]
    public ActionResult<List<JobResponse>> EditJob(int id, JobRequest request)
    {
        var persistedJob = _jobService.EditJob(id, _mapper.Map<Job>(request));
        var job = _mapper.Map<JobResponse>(persistedJob);
        return Ok(job);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [JobNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<List<JobResponse>> DeleteJob(int id)
    {
        _jobService.DeleteJob(id);
        return NoContent();
    }
}