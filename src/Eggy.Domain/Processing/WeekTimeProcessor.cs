using Eggy.Domain.Brokers;
using Eggy.Domain.Models;

namespace Eggy.Domain.Processing;

public class WeekTimeProcessor : IWeekTimeProcessor
{
    private readonly IStorageBroker _storageBroker;

    public WeekTimeProcessor(IStorageBroker storageBroker) => _storageBroker = storageBroker;

    public WeekTimeEntry WeekTime { get; private set; } = new();

    public event Action? EntriesChanged;

    public ValueTask Load(DateTime? date = default)
    {
        //date ??= DateTime.Today;

        //WeekTime = await _storageBroker.GetDayEntry(DateOnly.FromDateTime(date.Value)).NoContext();

        //EntriesChanged?.Invoke();

        var project1 = new Project("Project1", "First Project", "DRD", "Development");
        var project2 = new Project("Project2", "Second Project", "SZT", "Admin");

        var weekTimeEntry = WeekTime = WeekTimeEntry.Generate(new DateOnly(2022, 02, 21));

        weekTimeEntry.ProjectEntries = new()
        {
            new()
            {
                Project = project1,
                TimeEntries = new()
                {
                    new() {Date = new(2022, 02, 21), Comment = "Monday 1", Hours = 1.5},
                    new() {Date = new(2022, 02, 22), Comment = "Tuesday 1", Hours = 2},
                    new() {Date = new(2022, 02, 23), Comment = "Wednesday 1", Hours = 3.5},
                    new() {Date = new(2022, 02, 24), Comment = "Thursday 1", Hours = 4},
                    new() {Date = new(2022, 02, 25), Comment = "Friday 1", Hours = 5.5},
                    new() {Date = new(2022, 02, 26), Comment = "Saturday 1", Hours = 6},
                    new() {Date = new(2022, 02, 27), Comment = "Sunday 1", Hours = 7.5}
                }
            },
            new()
            {
                Project = project2,
                TimeEntries = new()
                {
                    new() {Date = new(2022, 02, 21), Comment = "Monday 2", Hours = 7},
                    new() {Date = new(2022, 02, 22), Comment = "Tuesday 2", Hours = 6.6},
                    new() {Date = new(2022, 02, 23), Comment = "Wednesday 3", Hours = 5},
                    new() {Date = new(2022, 02, 24), Comment = "Thursday 4", Hours = 4.5},
                    new() {Date = new(2022, 02, 25), Comment = "Friday 5", Hours = 3},
                    new() {Date = new(2022, 02, 26), Comment = "Saturday 6", Hours = 2.5},
                    new() {Date = new(2022, 02, 27), Comment = "Sunday 7", Hours = 1}
                }
            }
        };
        
        return ValueTask.CompletedTask;
    }
}