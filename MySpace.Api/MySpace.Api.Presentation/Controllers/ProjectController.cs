using System.Net;
using AutoMapper;
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
 
    [HttpGet("{id:guid}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> GetProject(Guid id)
    {
        var project = _mapper.Map<ProjectResponse>(_projectService.GetProject(new ProjectId(id)));
        return Ok(project);
    }
    
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> AddProject(ProjectRequest request)
    {
        var persistedProject = _projectService.AddProject(_mapper.Map<Project>(request));
        var project = _mapper.Map<ProjectResponse>(persistedProject);
        return Created(project.Id.ToString(), project);
    }

    [HttpPut("{id:guid}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ProjectResponse))]
    public ActionResult<ProjectResponse> EditProject(Guid id, ProjectRequest request)
    {
        var project = _mapper.Map<ProjectResponse>(_projectService.EditProject(new ProjectId(id), _mapper.Map<Project>(request)));
        return Ok(project);
    }
   
    [HttpDelete("{id:guid}")]
    [ProjectNotFoundExceptionFilter]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public ActionResult<ProjectResponse> DeleteProject(Guid id)
    {
        _projectService.DeleteProject(new ProjectId(id));
        return NoContent();
    }
}