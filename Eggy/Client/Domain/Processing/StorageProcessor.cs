using Eggy.Client.Domain.Brokers;
using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Processing;

internal class StorageProcessor : IStorageProcessor
{
    private readonly IStorageBroker _storageBroker;
    private List<Project> _allProjects = new(0);
    private List<string> _allProjectTypes = new(0);

    public StorageProcessor(IStorageBroker storageBroker) => _storageBroker = storageBroker;

    public IReadOnlyList<Project> AllProjects => _allProjects;

    public IReadOnlyList<string> AllProjectTypes => _allProjectTypes;

    public WeekTimeEntry WeekTimeEntries { get; private set; } = new();

    public event Action? ProjectsChanged;

    public async ValueTask Init()
    {
        _allProjects = await _storageBroker.GetAllProjects();
        _allProjectTypes = await _storageBroker.GetAllProjectTypes();
        ProjectsChanged?.Invoke();

        await Load();
    }

    public async ValueTask Load(DateTime? date = default)
    {
        date ??= DateTime.Today;

        WeekTimeEntries = await _storageBroker.GetDayEntry(DateOnly.FromDateTime(date.Value));
    }

    public async ValueTask AddProject(Project project)
    {
        _allProjects.Add(project);

        await _storageBroker.SaveProjects(_allProjects);

        ProjectsChanged?.Invoke();
    }

    public async ValueTask RemoveProject(Project project)
    {
        _allProjects.Remove(project);

        await _storageBroker.SaveProjects(_allProjects);

        ProjectsChanged?.Invoke();
    }

    public async ValueTask AddProjectType(string projectType)
    {
        _allProjectTypes.Add(projectType);

        await _storageBroker.SaveProjectTypes(_allProjectTypes);

        ProjectsChanged?.Invoke();
    }

    public async ValueTask RemoveProjectType(string projectType)
    {
        _allProjectTypes.Remove(projectType);

        await _storageBroker.SaveProjectTypes(_allProjectTypes);

        ProjectsChanged?.Invoke();
    }
}