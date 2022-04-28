using Eggy.Domain.Models;

namespace Eggy.Domain.Brokers;

public interface IStorageBroker
{
    WeekTimeEntry GetDayEntry(DateOnly? dateOnly = default);

    List<Project> GetAllProjects();

    List<ProjectType> GetAllProjectTypes();

    void SaveProjects(List<Project> projects);

    void SaveProjectTypes(List<ProjectType> allProjectTypes);
}