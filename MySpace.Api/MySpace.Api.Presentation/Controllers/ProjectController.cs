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
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;
    private readonly HashIdUtil _hashIdUtil;

    public ProjectController(IProjectService projectService, IMapper mapper, HashIdUtil hashIdUtil)
    {
        _projectService = projectService;
        _mapper = mapper;
        _hashIdUtil = hashIdUtil;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<ProjectResponse>))]
    public ActionResult<List<ProjectResponse>> GetProjects()
    {
        var projects = _projectService.GetProjects().Select(MapToProjectResponse).ToList();
        return Ok(projects);
    }
 
    [HttpGet("{id}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> GetProject(string id)
    {
        var project = MapToProjectResponse(_projectService.GetProject(_hashIdUtil.DecodeId(id)));
        return Ok(project);
    }
    
    [Authorize]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> AddProject(ProjectRequest request)
    {
        var persistedProject = _projectService.AddProject(_mapper.Map<Project>(request));
        var project = MapToProjectResponse(persistedProject);
        return Created(project.Id, project);
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> EditProject(string id, ProjectRequest request)
    {
        var project = MapToProjectResponse(_projectService.EditProject(_hashIdUtil.DecodeId(id), _mapper.Map<Project>(request)));
        return Ok(project);
    }
   
    [Authorize]
    [HttpDelete("{id}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<ProjectResponse> DeleteProject(string id)
    {
        _projectService.DeleteProject(_hashIdUtil.DecodeId(id));
        return NoContent();
    }
    
    private ProjectResponse MapToProjectResponse(Project project)
    {
        var projectResponse = _mapper.Map<ProjectResponse>(project);
        projectResponse.Id = _hashIdUtil.EncodeId(project.Id.Value);
        return projectResponse;
    }
}