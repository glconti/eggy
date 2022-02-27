namespace Eggy.Client.Domain.Models;

public class DayTimeEntry
{
    public DateOnly Date { get; set; }

    public List<TimeEntry> TimeEntries { get; set; } = new();

    public double TotalHours => TimeEntries.Sum(t => t.Hours);
}