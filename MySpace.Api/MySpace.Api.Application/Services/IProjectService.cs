using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Services;

public interface IProjectService
{
    List<Project> GetProjects();
    Project GetProject(int id);
    Project AddProject(Project project);
    Project EditProject(int id, Project project);
    void DeleteProject(int id);
}