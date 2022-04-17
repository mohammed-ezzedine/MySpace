using MySpace.Api.Application.Exceptions;
using MySpace.Api.Application.Persistence;
using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public class ProjectService : IProjectService
{
    private readonly ProjectRepository _projectRepository;

    public ProjectService(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public List<Project> GetProjects()
    {
        return _projectRepository.GetProjects();
    }

    public Project GetProject(int id)
    {
        ThrowExceptionIfProjectNotFound(id);
        return _projectRepository.GetProject(id);
    }

    public Project AddProject(Project project)
    {
        project.CreatedDate ??= DateTime.Now;
        return _projectRepository.AddProject(project);
    }

    public Project EditProject(int id, Project project)
    {
        ThrowExceptionIfProjectNotFound(id);
        return _projectRepository.EditProject(id, project);
    }

    public void DeleteProject(int id)
    {
        ThrowExceptionIfProjectNotFound(id);
        _projectRepository.DeleteProject(id);
    }

    private void ThrowExceptionIfProjectNotFound(int id)
    {
        if (!_projectRepository.ProjectExists(id))
        {
            throw new ProjectNotFoundException(id.ToString());
        }
    }
}