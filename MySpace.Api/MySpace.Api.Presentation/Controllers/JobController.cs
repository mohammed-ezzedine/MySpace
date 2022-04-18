using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpace.Api.Application.Services;
using MySpace.Api.Domain.Models;
using MySpace.Api.Presentation.Filters;
using MySpace.Api.Presentation.Requests;
using MySpace.Api.Presentation.Responses;
using MySpace.Api.Presentation.Utils;

namespace MySpace.Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IMapper _mapper;
    private readonly HashIdUtil _hashIdUtil;

    public JobController(IJobService jobService, IMapper mapper, HashIdUtil hashIdUtil)
    {
        _jobService = jobService;
        _mapper = mapper;
        _hashIdUtil = hashIdUtil;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<JobResponse>))]
    public ActionResult<List<JobResponse>> GetJobs()
    {
        var jobs = _jobService.GetJobs().Select(MapToJobResponse).ToList();
        return Ok(jobs);
    }
    
    [HttpGet("current")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(JobResponse))]
    public ActionResult<List<JobResponse>> GetCurrentJob()
    {
        var job = MapToJobResponse(_jobService.GetCurrentJob());
        return Ok(job);
    }

    [HttpGet("{id}")]
    [JobNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(JobResponse))]
    public ActionResult<List<JobResponse>> GetJob(string id)
    {
        var job = MapToJobResponse(_jobService.GetJob(_hashIdUtil.DecodeId(id)));
        return Ok(job);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(JobResponse))]
    public ActionResult<List<JobResponse>> AddJob(JobRequest request)
    {
        var persistedJob = _jobService.AddJob(_mapper.Map<Job>(request));
        var job = MapToJobResponse(persistedJob);
        return Created(job.Id, job);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    [JobNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<JobResponse>))]
    public ActionResult<List<JobResponse>> EditJob(string id, JobRequest request)
    {
        var persistedJob = _jobService.EditJob(_hashIdUtil.DecodeId(id), _mapper.Map<Job>(request));
        var job = MapToJobResponse(persistedJob);
        return Ok(job);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [JobNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<List<JobResponse>> DeleteJob(string id)
    {
        _jobService.DeleteJob(_hashIdUtil.DecodeId(id));
        return NoContent();
    }
    
    private JobResponse MapToJobResponse(Job job)
    {
        var jobResponse = _mapper.Map<JobResponse>(job);
        jobResponse.Id = _hashIdUtil.EncodeId(job.Id.Value);
        return jobResponse;
    }
}