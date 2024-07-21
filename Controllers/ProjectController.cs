using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using TaskManager.Interface;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProject _project;
        public ProjectController(IProject project)
        {
            this._project = project;

        }
        [HttpGet("AllProjects")]

        public IActionResult AllProjects() 
        {
            var allprojects = _project.AllProjects().ToList();
            return Ok(allprojects);
        }

        [HttpPost("AddProject")]

        public IActionResult AddProject([FromBody] ProjectDto project)
        {
            var addedProject = _project.AddProject(project);
            return Ok($"{addedProject.Name} project has been added");
        }
        [HttpPost("DeleteProject")]
        public IActionResult deleteproject(int id)
        {
            var deletedproject = _project.DeleteProject(id);
            return Ok($"{deletedproject.Name} has been deleted ");
        }
        [HttpPost("updateProject")]

        public IActionResult updateproject([FromBody] ProjectDto updateproject)
        {
            var update =_project.UpdateProject(updateproject);
            return Ok($"{update.Name} project has been added");
        }
        [HttpGet("searchname")]

        public IActionResult Search(string name)
        {
            var searchedproject = _project.ProjectByName(name);
            return Ok(searchedproject);
        }

    }
}
