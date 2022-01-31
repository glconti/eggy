using Eggy.Client.Domain.Brokers;
using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Processing;

internal class StorageProcessor : IStorageProcessor
{
    private readonly IStorageBroker _storageBroker;
    private List<Project> _allProjects = new(0);

    public StorageProcessor(IStorageBroker storageBroker)
    {
        _storageBroker = storageBroker;
    }

    public IReadOnlyList<Project> AllProjects => _allProjects;

    public async ValueTask Init()
    {
        _allProjects = await _storageBroker.GetAllProjects();
        await Load();
    }

    public async ValueTask Load(DateTime? date = default)
    {
        date ??= DateTime.Today;

        _allProjects = await _storageBroker.GetAllProjects();
        WeekTimeEntries = await _storageBroker.GetDayEntry(DateOnly.FromDateTime(date.Value));
    }

    public async ValueTask SaveProject(Project project)
    {
        _allProjects.Add(project);

        await _storageBroker.SaveProject(_allProjects);

        ProjectsChanged?.Invoke();
    }

    public WeekTimeEntry WeekTimeEntries { get; private set; } = new();

    public event Action? ProjectsChanged;
}