using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Processing;

internal interface IProjectProcessor
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