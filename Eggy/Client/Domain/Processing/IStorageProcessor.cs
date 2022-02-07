using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Processing;

internal interface IStorageProcessor
{
    IReadOnlyList<Project> AllProjects { get; }

    IReadOnlyList<string> AllProjectTypes { get; }

    WeekTimeEntry WeekTimeEntries { get; }

    ValueTask Init();

    ValueTask Load(DateTime? date = default);

    ValueTask AddProject(Project project);

    ValueTask RemoveProject(Project project);

    ValueTask AddProjectType(string newType);

    ValueTask RemoveProjectType(string newType);

    event Action ProjectsChanged;
}