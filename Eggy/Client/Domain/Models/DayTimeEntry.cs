namespace Eggy.Client.Domain.Models;

internal class DayTimeEntry
{
    public DateOnly Date { get; set; }

    public List<TimeEntry> TimeEntries { get; set; } = new();
}