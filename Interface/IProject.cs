using Microsoft.Build.Evaluation;
using TaskManager.Models;
namespace TaskManager.Interface
{
    public interface IProject
    {
        public IEnumerable<ProjectDto> AllProjects();
        public IEnumerable<ProjectDto> ProjectByName(string name);
        public ProjectDto AddProject (ProjectDto project);
        public Project1 DeleteProject (int id);
        public ProjectDto UpdateProject (ProjectDto project);

    }
}
