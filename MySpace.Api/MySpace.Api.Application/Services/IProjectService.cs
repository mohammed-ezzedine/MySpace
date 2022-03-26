using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface IProjectService
{
    List<Project> GetProjects();
    Project GetProject(ProjectId id);
    Project AddProject(Project project);
    Project EditProject(ProjectId id, Project project);
    void DeleteProject(ProjectId id);
}