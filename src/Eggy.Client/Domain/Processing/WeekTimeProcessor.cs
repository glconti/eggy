using Eggy.Client.Domain.Brokers;
using Eggy.Client.Domain.Models;
using Eggy.Client.Domain.System;

namespace Eggy.Client.Domain.Processing;

internal class WeekTimeProcessor : IWeekTimeProcessor
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