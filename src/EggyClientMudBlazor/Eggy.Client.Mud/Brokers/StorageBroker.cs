using Eggy.Domain.Brokers;
using Eggy.Domain.Models;
using Microsoft.JSInterop;

namespace Eggy.Client.Mud.Brokers;

internal class StorageBroker : IStorageBroker
{
    private const string ProjectListStorage = "Const_ProjectsList";
    private const string ProjectTypesListStorage = "Const_ProjectTypesList";

    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<StorageBroker> _logger;

    public StorageBroker(ILocalStorageService localStorage, ILogger<StorageBroker> logger)
    {
        _localStorage = localStorage;
        _logger = logger;
    }

    public WeekTimeEntry GetDayEntry(DateOnly? dateOnly = default)
    {
        dateOnly ??= DateOnly.FromDateTime(DateTime.UtcNow.Date);

        return _localStorage.GetItem<WeekTimeEntry>(WeekTimeEntry.GetKey(dateOnly.Value)) ??
               WeekTimeEntry.Generate(dateOnly);
    }

    public List<Project> GetAllProjects()
    {
        try
        {
            return _localStorage.GetItem<List<Project>>(ProjectListStorage) ?? new List<Project>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to load project list. It will be reset");

            var projects = new List<Project>();
            SaveProjects(projects);
            return projects;
        }
    }

    public List<ProjectType> GetAllProjectTypes()
    {
        try
        {
            return _localStorage.GetItem<List<ProjectType>>(ProjectTypesListStorage)
                   ?? new List<ProjectType>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to load project types list. It will be reset");

            var projectTypes = new List<ProjectType>();
            SaveProjectTypes(projectTypes);
            return projectTypes;
        }
    }

    public void SaveProjects(List<Project> projects) => _localStorage.SetItem("Const_ProjectsList", projects);

    public void SaveProjectTypes(List<ProjectType> allProjectTypes) => _localStorage.SetItem(ProjectTypesListStorage, allProjectTypes);
}