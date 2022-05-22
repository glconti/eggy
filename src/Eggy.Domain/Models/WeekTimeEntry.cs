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
        var projectEntry = GenerateProjectEntry(date.Value);
        weekTimeEntry.ProjectEntries.Add(projectEntry);
        weekTimeEntry.DaysOfTheWeek.AddRange(projectEntry.TimeEntries.Select(x => x.Date));

        return weekTimeEntry;
    }

    public static string GetKey(DateOnly dateOnly) => $"{dateOnly.Year}-{GetWeekOfYear(dateOnly)}";

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);

    private static ProjectTimeEntry GenerateProjectEntry(DateOnly dateTime)
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