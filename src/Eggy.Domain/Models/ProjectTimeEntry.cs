namespace Eggy.Domain.Models;

public class ProjectTimeEntry
{
    public Project Project { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);

    public List<TimeEntry> TimeEntries { get; set; } = new(7);

    public double TotalHours => TimeEntries.Sum(t => t.Hours);

    public TimeEntry Monday => TimeEntries[0];

    public TimeEntry Tuesday => TimeEntries[1];

    public TimeEntry Wednesday => TimeEntries[2];

    public TimeEntry Thursday => TimeEntries[3];

    public TimeEntry Friday => TimeEntries[4];

    public TimeEntry Saturday => TimeEntries[5];

    public TimeEntry Sunday => TimeEntries[6];
}