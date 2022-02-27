using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Processing;

internal interface IWeekTimeProcessor
{
    WeekTimeEntry WeekTimeEntries { get; }

    event Action? EntriesChanged;

    ValueTask Load(DateTime? date = default);
}