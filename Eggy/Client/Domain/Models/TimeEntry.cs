namespace Eggy.Client.Domain.Models;

internal class TimeEntry
{
    public Project Project { get; set; }

    public double Hours { get; set; }

    public string Comment { get; set; }
}