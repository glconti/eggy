using Eggy.Domain.Models;

namespace Eggy.Domain.Processing;

public interface IWeekTimeProcessor
{
    WeekTimeEntry WeekTime { get; }

    event Action? EntriesChanged;

    ValueTask Load(DateTime? date = default);
}