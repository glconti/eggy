using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eggy.Domain.Models;

public class WeekTimeEntry
{
    [Key]
    public string Id { get; set; } // year-weekofyear

    public List<DayTimeEntry> TimeEntries { get; set; } = new();

    public IEnumerable<Project> AllProjects =>
        TimeEntries.SelectMany(x => x.TimeEntries.Select(t => t.Project)).Distinct();

    public static WeekTimeEntry Generate(DateOnly? date = default)
    {
        date ??= DateOnly.FromDateTime(DateTime.UtcNow);

        return new()
        {
            Id = GetKey(date.Value),
            TimeEntries = GenerateWeek(date.Value)
        };
    }

    public static string GetKey(DateOnly dateOnly) => $"{dateOnly.Year}-{GetWeekOfYear(dateOnly)}";

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new(0, 0), DateTimeKind.Utc),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);

    private static List<DayTimeEntry> GenerateWeek(DateOnly dateTime)
    {
        var prevMondayShift = -((dateTime.DayOfWeek - DayOfWeek.Monday + 7) % 7);
        var startDate = dateTime.AddDays(prevMondayShift);

        var result = new List<DayTimeEntry>(7);

        for (var i = 0; i < 7; i++)
        {
            result.Add(new()
            {
                Date = startDate
            });

            startDate = startDate.AddDays(1);
        }

        return result;
    }
};