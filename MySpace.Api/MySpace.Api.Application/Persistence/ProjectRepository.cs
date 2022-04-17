using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface ProjectRepository
{
    List<Project> GetProjects();
    Project GetProject(int id);
    Project AddProject(Project project);
    Project EditProject(int id, Project project);
    void DeleteProject(int id);
    bool ProjectExists(int id);
}