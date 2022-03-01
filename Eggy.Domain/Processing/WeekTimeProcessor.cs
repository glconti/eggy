using Eggy.Domain.Brokers;
using Eggy.Domain.Models;
using Eggy.Domain.System;

namespace Eggy.Domain.Processing;

public class WeekTimeProcessor : IWeekTimeProcessor
{
    private readonly IStorageBroker _storageBroker;

    public WeekTimeProcessor(IStorageBroker storageBroker)
    {
        _storageBroker = storageBroker;
    }

    public WeekTimeEntry WeekTimeEntries { get; private set; } = new();

    public event Action? EntriesChanged;

    public async ValueTask Load(DateTime? date = default)
    {
        date ??= DateTime.Today;

        WeekTimeEntries = await _storageBroker.GetDayEntry(DateOnly.FromDateTime(date.Value)).NoContext();

        EntriesChanged?.Invoke();
    }
}