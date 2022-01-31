using System.ComponentModel.DataAnnotations;

namespace Eggy.Client.Domain.Models;

internal class WeekTimeEntry
{
    [Key]
    public long Id { get; set; } // year-dayofyear

    public List<DayTimeEntry> TimeEntries { get; set; } = new();
}