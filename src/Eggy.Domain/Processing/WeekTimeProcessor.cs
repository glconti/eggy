using Eggy.Domain.Brokers;
using Eggy.Domain.Models;

namespace Eggy.Domain.Processing;

public class WeekTimeProcessor : IWeekTimeProcessor
{
    private readonly IStorageBroker _storageBroker;

    public WeekTimeProcessor(IStorageBroker storageBroker) => _storageBroker = storageBroker;

    public WeekTimeEntry WeekTime { get; private set; } = new();

    public event Action? EntriesChanged;

    public async ValueTask Load(DateTime? date = default)
    {
        //date ??= DateTime.Today;

        //WeekTime = await _storageBroker.GetDayEntry(DateOnly.FromDateTime(date.Value)).NoContext();

        //EntriesChanged?.Invoke();

        var project1 = new Project("Project1", "First Project", "DRD", "Development");
        var project2 = new Project("Project2", "Second Project", "SZT", "Admin");

        WeekTime = new()
        {
            Id = "2022-9",
            TimeEntries = new()
            {
                new()
                {
                    Date = new(2022, 02, 21),
                    TimeEntries = new()
                    {
                        new() { Comment = "A", Hours = 3, Project = project1 },
                        new() { Comment = "B", Hours = 2, Project = project2 }
                    }
                },
                new()
                {
                    Date = new(2022, 02, 22),
                    TimeEntries = new()
                    {
                        new() { Comment = "C", Hours = 1, Project = project1 },
                        new() { Comment = "D", Hours = 2, Project = project2 }
                    }
                },
                new()
                {
                    Date = new(2022, 02, 23),
                    TimeEntries = new()
                    {
                        new() { Comment = "E", Hours = 1, Project = project2 },
                        new() { Comment = "F", Hours = 0.5, Project = project1 }
                    }
                },
                new()
                {
                    Date = new(2022, 02, 24),
                    TimeEntries = new()
                    {
                        new() { Comment = "G", Hours = 2, Project = project1 },
                        new() { Comment = "H", Hours = 1, Project = project2 }
                    }
                },
                new()
                {
                    Date = new(2022, 02, 25),
                    TimeEntries = new()
                    {
                        new() { Comment = "J", Hours = 4, Project = project1 },
                        new() { Comment = "K", Hours = 3, Project = project2 }
                    }
                },
                new() { Date = new(2022, 02, 26) },
                new() { Date = new(2022, 02, 27) }
            }
        };
    }
}