using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eggy.Domain.Models;

public class WeekTimeEntry
{
    [Key]
    public string Id { get; set; } = string.Empty; // year-weekofyear

    public List<ProjectTimeEntry> ProjectEntries { get; set; } = new();

    public List<DateOnly> DaysOfTheWeek { get; } = new(7);

    public static WeekTimeEntry Generate(DateOnly? date = default)
    {
        date ??= DateOnly.FromDateTime(DateTime.UtcNow);

        var weekTimeEntry = new WeekTimeEntry
        {
            Id = GetKey(date.Value)
        };
        var projectEntry = ProjectTimeEntry.Generate(date.Value);
        weekTimeEntry.ProjectEntries.Add(projectEntry);
        weekTimeEntry.DaysOfTheWeek.AddRange(projectEntry.TimeEntries.Select(x => x.Date));

        return weekTimeEntry;
    }

    public static string GetKey(DateOnly dateOnly) => $"{dateOnly.Year}-{GetWeekOfYear(dateOnly)}";

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new(0, 0), DateTimeKind.Utc),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);
}