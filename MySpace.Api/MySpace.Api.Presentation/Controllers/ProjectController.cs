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
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(List<ProjectResponse>))]
    public ActionResult<List<ProjectResponse>> GetProjects()
    {
        var projects = _projectService.GetProjects().Select(_mapper.Map<ProjectResponse>).ToList();
        return Ok(projects);
    }
 
    [HttpGet("{id:int}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> GetProject(int id)
    {
        var project = _mapper.Map<ProjectResponse>(_projectService.GetProject(id));
        return Ok(project);
    }
    
    [Authorize]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> AddProject(ProjectRequest request)
    {
        var persistedProject = _projectService.AddProject(_mapper.Map<Project>(request));
        var project = _mapper.Map<ProjectResponse>(persistedProject);
        return Created(project.Id.ToString(), project);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> EditProject(int id, ProjectRequest request)
    {
        var project = _mapper.Map<ProjectResponse>(_projectService.EditProject(id, _mapper.Map<Project>(request)));
        return Ok(project);
    }
   
    [Authorize]
    [HttpDelete("{id:int}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<ProjectResponse> DeleteProject(int id)
    {
        _projectService.DeleteProject(id);
        return NoContent();
    }
}