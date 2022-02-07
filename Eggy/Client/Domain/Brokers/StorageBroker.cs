using System.Globalization;
using Blazored.LocalStorage;
using Eggy.Client.Domain.Models;
using Eggy.Client.Domain.System;

namespace Eggy.Client.Domain.Brokers;

internal class StorageBroker : IStorageBroker
{
    private const string ProjectListStorage = "Const_ProjectsList";
    private const string ProjectTypesListStorage = "Const_ProjectsList";

    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<StorageBroker> _logger;

    public StorageBroker(ILocalStorageService localStorage, ILogger<StorageBroker> logger)
    {
        _localStorage = localStorage;
        _logger = logger;
    }

    public async ValueTask<WeekTimeEntry> GetDayEntry(DateOnly? dateOnly = default)
    {
        dateOnly ??= DateOnly.FromDateTime(DateTime.Today);

        return await _localStorage.GetItemAsync<WeekTimeEntry>(
            $"{dateOnly.Value.Year}-{GetWeekOfYear(dateOnly.Value)}").NoContext() ?? new WeekTimeEntry();
    }

    public async ValueTask<List<Project>> GetAllProjects()
    {
        try
        {
            return await _localStorage.GetItemAsync<List<Project>>(ProjectListStorage).NoContext() ?? new List<Project>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to load project list. It will be reset");

            var projects = new List<Project>();
            await SaveProjects(projects).NoContext();
            return projects;
        }
    }

    public async ValueTask<List<ProjectType>> GetAllProjectTypes()
    {
        try
        {
            return await _localStorage.GetItemAsync<List<ProjectType>>(ProjectTypesListStorage)
                .NoContext() ?? new List<ProjectType>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to load project types list. It will be reset");

            var projectTypes = new List<ProjectType>();
            await SaveProjectTypes(projectTypes).NoContext();
            return projectTypes;
        }
    }

    public ValueTask SaveProjects(List<Project> projects) =>  _localStorage.SetItemAsync("Const_ProjectsList", projects);

    public ValueTask SaveProjectTypes(List<ProjectType> allProjectTypes) => _localStorage.SetItemAsync(ProjectTypesListStorage, allProjectTypes);

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new TimeOnly(0, 0)),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);
}