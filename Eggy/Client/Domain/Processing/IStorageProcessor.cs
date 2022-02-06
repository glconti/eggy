using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Processing;

internal interface IStorageProcessor
{
    IReadOnlyList<Project> AllProjects { get; }

    WeekTimeEntry WeekTimeEntries { get; }

    ValueTask Init();

    ValueTask Load(DateTime? date = default);

    ValueTask AddProject(Project project);

    ValueTask RemoveProject(Project project);

    event Action ProjectsChanged;
}