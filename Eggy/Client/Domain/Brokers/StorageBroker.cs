﻿using System.Globalization;
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

    public async ValueTask<List<Project>> GetAllProjects() => await _localStorage.GetItemAsync<List<Project>>("Const_ProjectsList") ?? new List<Project>();

    public async ValueTask<List<string>> GetAllProjectTypes() => await _localStorage.GetItemAsync<List<string>>("Const_ProjectsTypesList") ?? new List<string>();

    public ValueTask SaveProjects(List<Project> projects) =>  _localStorage.SetItemAsync("Const_ProjectsList", projects);

    public ValueTask SaveProjectTypes(List<string> allProjectTypes) => _localStorage.SetItemAsync("Const_ProjectsTypesList", allProjectTypes);

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new TimeOnly(0, 0)),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);
}