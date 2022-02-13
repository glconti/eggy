using Eggy.Client.Domain.Brokers;
using Eggy.Client.Domain.Models;
using Eggy.Client.Domain.System;

namespace Eggy.Client.Domain.Processing;

internal class ProjectProcessor : IProjectProcessor
{
    private readonly IStorageBroker _storageBroker;
    private List<Project> _allProjects = new(0);
    private List<ProjectType> _allProjectTypes = new(0);

    public ProjectProcessor(IStorageBroker storageBroker) => _storageBroker = storageBroker;

    public IReadOnlyList<Project> AllProjects => _allProjects;

    public IReadOnlyList<ProjectType> AllProjectTypes => _allProjectTypes;

    public WeekTimeEntry WeekTimeEntries { get; private set; } = new();

    public event Action? ProjectsChanged;

    public async ValueTask Init()
    {
        _allProjects = await _storageBroker.GetAllProjects().NoContext();
        _allProjectTypes = await _storageBroker.GetAllProjectTypes().NoContext();
        ProjectsChanged?.Invoke();

        await Load();
    }

    public async ValueTask Load(DateTime? date = default)
    {
        date ??= DateTime.Today;

        WeekTimeEntries = await _storageBroker.GetDayEntry(DateOnly.FromDateTime(date.Value)).NoContext();
    }

    public async ValueTask AddProject(Project project)
    {
        _allProjects.Add(project);

        await _storageBroker.SaveProjects(_allProjects).NoContext();

        ProjectsChanged?.Invoke();
    }

    public async ValueTask RemoveProject(Project project)
    {
        _allProjects.Remove(project);

        await _storageBroker.SaveProjects(_allProjects).NoContext();

        ProjectsChanged?.Invoke();
    }

    public async ValueTask AddProjectType(ProjectType projectType)
    {
        _allProjectTypes.Add(projectType);

        await _storageBroker.SaveProjectTypes(_allProjectTypes).NoContext();

        ProjectsChanged?.Invoke();
    }

    public async ValueTask RemoveProjectType(ProjectType projectType)
    {
        _allProjectTypes.Remove(projectType);

        await _storageBroker.SaveProjectTypes(_allProjectTypes).NoContext();

        ProjectsChanged?.Invoke();
    }
}