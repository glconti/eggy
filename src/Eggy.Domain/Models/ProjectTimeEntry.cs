namespace Eggy.Domain.Models;

public class ProjectTimeEntry
{
    public Project Project { get; set; } = Project.Empty;

    public List<TimeEntry> TimeEntries { get; set; } = new(7);

    public double TotalHours => TimeEntries.Sum(t => t.Hours);

    public TimeEntry Monday => TimeEntries[0];

    public TimeEntry Tuesday => TimeEntries[1];

    public TimeEntry Wednesday => TimeEntries[2];

    public TimeEntry Thursday => TimeEntries[3];

    public TimeEntry Friday => TimeEntries[4];

    public TimeEntry Saturday => TimeEntries[5];

    public TimeEntry Sunday => TimeEntries[6];

    public static ProjectTimeEntry Generate(DateOnly dateTime)
    {
        var prevMondayShift = -((dateTime.DayOfWeek - DayOfWeek.Monday + 7) % 7);
        var startDate = dateTime.AddDays(prevMondayShift);

        var result = new ProjectTimeEntry();

        for (var i = 0; i < 7; i++)
        {
            result.TimeEntries.Add(new()
            {
                Date = startDate
            });

            startDate = startDate.AddDays(1);
        }

        return result;
    }
}