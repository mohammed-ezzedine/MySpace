using MySpace.Api.Domain.Models;

namespace MySpace.Api.Application.Persistence;

public interface ProjectRepository
{
    List<Project> GetProjects();
    Project GetProject(ProjectId id);
    Project AddProject(Project project);
    Project EditProject(ProjectId id, Project project);
    void DeleteProject(ProjectId id);
    bool ProjectExists(ProjectId id);
}