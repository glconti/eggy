using Eggy.Client.Domain.Models;

namespace Eggy.Client.Domain.Brokers;

internal interface IStorageBroker
{
    ValueTask<WeekTimeEntry> GetDayEntry(DateOnly? dateOnly = default);

    ValueTask<List<Project>> GetAllProjects();

    ValueTask<List<ProjectType>> GetAllProjectTypes();

    ValueTask SaveProjects(List<Project> projects);

    ValueTask SaveProjectTypes(List<ProjectType> allProjectTypes);
}