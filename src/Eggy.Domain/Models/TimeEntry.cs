namespace Eggy.Domain.Models;

public class TimeEntry
{
    public DateOnly Date { get; set; }

    public double Hours { get; set; }

    public string Comment { get; set; } = string.Empty;
}