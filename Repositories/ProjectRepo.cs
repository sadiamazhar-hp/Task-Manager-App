using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using System.Threading.Tasks;
using TaskManager.Interface;
using TaskManager.Models;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace TaskManager.Repositories
{
    public class ProjectRepo : IProject
    {
        public readonly ITaskRepo _taskRepo;
        public readonly IUser _userdata;
        List<Project1> _projects = new()
        {
            new Project1{ Id = 0, Name ="Weatherapp", Description="weather forecast displaying app of all over the globe",
            TasksId=[0,1], Members=[0,3]},
            new Project1{ Id = 1, Name ="resturauntmario", Description="Webapp for mario resturaunt",
            TasksId=[0,1], Members=[0,3]},

        };
        public ProjectRepo(ITaskRepo taskRepo, IUser userdata ) 
        {
            this._taskRepo = taskRepo;
            this._userdata = userdata;   
        }


        public IEnumerable<ProjectDto> AllProjects()
        {
            return _projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                TaskTitle = p.TasksId
                .Select(taskId => _taskRepo.GetTasks().ToList().FirstOrDefault(t => t.Id == taskId)?.Title)
                .Where(title => title != null)
                .ToList(),
                Membernames = p.Members
                .Select(Member=>_userdata.AllUsers().ToList().FirstOrDefault(m=>m.ID==Member)?.UserName)
                .Where(member=>member!=null)
                .ToList()
            }).ToList();
        }

        public ProjectDto AddProject(ProjectDto projectDto)
        {
            // Generate new project ID based on the current count of projects
            int newProjectId = _projects.Count() + 1;

            // Map Task Titles to Task IDs
            var taskIds = projectDto.TaskTitle
                .Select(title => _taskRepo.GetTasks().FirstOrDefault(t => t.Title == title)?.Id)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            // Map Member Names to Member IDs
            var memberIds = projectDto.Membernames
                .Select(name => _userdata.AllUsers().FirstOrDefault(u => u.UserName == name)?.ID)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            // Create new Project1 object
            var newProject = new Project1
            {
                Id = newProjectId,
                Name = projectDto.Name,
                Description = projectDto.Description,
                TasksId = taskIds,
                Members = memberIds
            };

            // Add the new project to the repository
            _projects.Add(newProject);

            // Return the newly created project DTO
            return new ProjectDto
            {
                Id = newProject.Id,
                Name = newProject.Name,
                Description = newProject.Description,
                TaskTitle = projectDto.TaskTitle,
                Membernames = projectDto.Membernames
            };
        }

        public Project1 DeleteProject(int id)
        {
            var projecttodel =  _projects.Single(x=>x.Id == id);

            return projecttodel;
        }

        public IEnumerable<ProjectDto> ProjectByName(string name)
        {
            var projects = from p in _projects
                           where string.IsNullOrEmpty(name) || p.Name.StartsWith(name)
                           orderby p.Name
                           select new ProjectDto
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Description = p.Description,
                               TaskTitle = p.TasksId.Select(id => _taskRepo.GetTasks().FirstOrDefault(t => t.Id == id)?.Title),
                               Membernames = p.Members.Select(id => _userdata.AllUsers().FirstOrDefault(u => u.ID == id)?.UserName)
                           };
            return projects;
        }


        public ProjectDto UpdateProject(ProjectDto projectDto)
        {
            var projectToUpdate = _projects.FirstOrDefault(p => p.Id == projectDto.Id);
            if (projectToUpdate == null)
            {
                throw new KeyNotFoundException("Project not found.");
            }

            projectToUpdate.Name = projectDto.Name;
            projectToUpdate.Description = projectDto.Description;
            projectToUpdate.TasksId = projectDto.TaskTitle
                .Select(title => _taskRepo.GetTasks().FirstOrDefault(t => t.Title == title)?.Id)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();
            projectToUpdate.Members = projectDto.Membernames
                .Select(name => _userdata.AllUsers().FirstOrDefault(u => u.UserName == name)?.ID)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            // Optionally return the updated ProjectDto
            return new ProjectDto
            {
                Id = projectToUpdate.Id,
                Name = projectToUpdate.Name,
                Description = projectToUpdate.Description,
                TaskTitle = projectToUpdate.TasksId.Select(id => _taskRepo.GetTasks().FirstOrDefault(t => t.Id == id)?.Title),
                Membernames = projectToUpdate.Members.Select(id => _userdata.AllUsers().FirstOrDefault(u => u.ID == id)?.UserName)
            };
        }



    }
}
