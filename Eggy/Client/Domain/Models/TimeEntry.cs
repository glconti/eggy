namespace Eggy.Client.Domain.Models;

internal class TimeEntry
{
    public Project Project { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);

    public double Hours { get; set; }

    public string Comment { get; set; } = string.Empty;
}