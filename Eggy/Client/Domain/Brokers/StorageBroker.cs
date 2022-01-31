using System.Globalization;
using Blazored.LocalStorage;
using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Brokers;

internal class StorageBroker : IStorageBroker
{
    private readonly ILocalStorageService _localStorage;

    public StorageBroker(ILocalStorageService localStorage) => _localStorage = localStorage;

    public async ValueTask<WeekTimeEntry> GetDayEntry(DateOnly? dateOnly = default)
    {
        dateOnly ??= DateOnly.FromDateTime(DateTime.Today);

        return (await _localStorage.GetItemAsync<WeekTimeEntry>(
            $"{dateOnly.Value.Year}-{GetWeekOfYear(dateOnly.Value)}")) ?? new WeekTimeEntry();
    }

    public async ValueTask<List<Project>> GetAllProjects() => (await _localStorage.GetItemAsync<List<Project>>("Const_ProjectsList")) ?? new List<Project>();

    public ValueTask SaveProject(List<Project> project) =>  _localStorage.SetItemAsync("Const_ProjectsList", project);

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new TimeOnly(0, 0)),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);
}