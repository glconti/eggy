using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eggy.Client.Domain.Models;

internal class WeekTimeEntry
{
    [Key]
    public string Id { get; set; } // year-weekofyear

    public WeekTimeEntry()
    {
        Id = GetKey(DateOnly.FromDateTime(DateTime.UtcNow));
        TimeEntries = GenerateWeek(DateTime.UtcNow);
    }

    public List<DayTimeEntry> TimeEntries { get; } = new();

    public static string GetKey(DateOnly dateOnly) => $"{dateOnly.Year}-{GetWeekOfYear(dateOnly)}";

    private static int GetWeekOfYear(DateOnly dateOnly) =>
        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
            dateOnly.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc),
            CalendarWeekRule.FirstDay, DayOfWeek.Monday);

    private static List<DayTimeEntry> GenerateWeek(DateTime dateTime)
    {
        var result = new List<DayTimeEntry>(7);

        for (var i = 1; i < 8; i++)
        {
            var weekDay = dateTime.AddDays((DayOfWeek)i - dateTime.DayOfWeek);

            result.Add(new DayTimeEntry
            {
                Date = DateOnly.FromDateTime(weekDay)
            });
        }

        return result;
    }
};