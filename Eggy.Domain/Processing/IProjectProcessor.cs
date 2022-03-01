using Eggy.Domain.Models;

namespace Eggy.Domain.Processing;

public interface IProjectProcessor
{
    IReadOnlyList<Project> AllProjects { get; }

    IReadOnlyList<ProjectType> AllProjectTypes { get; }

    ValueTask Init();

    ValueTask AddProject(Project project);

    ValueTask RemoveProject(Project project);

    ValueTask AddProjectType(ProjectType newType);

    ValueTask RemoveProjectType(ProjectType newType);

    event Action ProjectsChanged;
}