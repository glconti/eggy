using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Brokers;

internal interface IStorageBroker
{
    ValueTask<WeekTimeEntry> GetDayEntry(DateOnly? dateOnly = default);

    ValueTask<List<Project>> GetAllProjects();

    ValueTask SaveProject(List<Project> project);
}