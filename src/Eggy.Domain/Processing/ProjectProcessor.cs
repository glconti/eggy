using Eggy.Domain.Brokers;
using Eggy.Domain.Models;

namespace Eggy.Domain.Processing;

public class ProjectProcessor : IProjectProcessor
{
    private readonly IStorageBroker _storageBroker;
    private List<Project> _allProjects = new(0);
    private List<ProjectType> _allProjectTypes = new(0);

    public ProjectProcessor(IStorageBroker storageBroker) => _storageBroker = storageBroker;

    public IReadOnlyList<Project> AllProjects => _allProjects;

    public IReadOnlyList<ProjectType> AllProjectTypes => _allProjectTypes;

    public event Action? ProjectsChanged;

    public void Init()
    {
        _allProjects = _storageBroker.GetAllProjects();
        _allProjectTypes = _storageBroker.GetAllProjectTypes();

        ProjectsChanged?.Invoke();
    }

    public void AddProject(Project project)
    {
        _allProjects.Add(project);

        _storageBroker.SaveProjects(_allProjects);

        ProjectsChanged?.Invoke();
    }

    public void RemoveProject(Project project)
    {
        _allProjects.Remove(project);

        _storageBroker.SaveProjects(_allProjects);

        ProjectsChanged?.Invoke();
    }

    public void AddProjectType(ProjectType projectType)
    {
        _allProjectTypes.Add(projectType);

        _storageBroker.SaveProjectTypes(_allProjectTypes);

        ProjectsChanged?.Invoke();
    }

    public void RemoveProjectType(ProjectType projectType)
    {
        _allProjectTypes.Remove(projectType);

        _storageBroker.SaveProjectTypes(_allProjectTypes);

        ProjectsChanged?.Invoke();
    }
}