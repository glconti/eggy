using Eggy.Domain.Models;

namespace Eggy.Domain.Processing;

public interface IProjectProcessor
{
    IReadOnlyList<Project> AllProjects { get; }

    IReadOnlyList<ProjectType> AllProjectTypes { get; }

    void Init();

    void AddProject(Project project);

    void RemoveProject(Project project);

    void AddProjectType(ProjectType newType);

    void RemoveProjectType(ProjectType newType);

    event Action ProjectsChanged;
}