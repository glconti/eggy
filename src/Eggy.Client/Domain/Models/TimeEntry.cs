namespace Eggy.Client.Domain.Models;

public class TimeEntry
{
    public Project Project { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);

    public double Hours { get; set; }

    public string Comment { get; set; } = string.Empty;
}