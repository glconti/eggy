using Eggy.Domain.Models;

namespace Eggy.Domain.Brokers;

public interface IStorageBroker
{
    ValueTask<WeekTimeEntry> GetDayEntry(DateOnly? dateOnly = default);

    ValueTask<List<Project>> GetAllProjects();

    ValueTask<List<ProjectType>> GetAllProjectTypes();

    ValueTask SaveProjects(List<Project> projects);

    ValueTask SaveProjectTypes(List<ProjectType> allProjectTypes);
}