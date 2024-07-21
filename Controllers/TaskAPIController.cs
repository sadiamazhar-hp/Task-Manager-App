using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Interface;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    
    [Route("api/Task")]
    [ApiController]
    public class TaskAPIController : ControllerBase
    {
        public readonly ITaskRepo _taskdata;
        public TaskAPIController(ITaskRepo taskdata) {
            this._taskdata = taskdata;
        }


        [HttpGet("Tasks")]
        
        public IActionResult AllTasks()
        {
            var Alltasks = _taskdata.GetTasks();
            return Ok(Alltasks) ;
        }

        [HttpPost("AddTask")]
        
        public IActionResult AddTask([FromBody]Tasks newtask)
        {
            var addtask = _taskdata.AddTask(newtask);
            return Ok(addtask);
        }

        [HttpDelete("DeleteTask")]
        
        public IActionResult DeleteTask([FromBody] int ID)
        {
            var DeleteTask = _taskdata.RemoveTask(ID);
            return Ok(DeleteTask);
        }

        [HttpPatch("UpdateTask")]
        

        public IActionResult UpdatedTask([FromBody] Tasks updatetask)
        {
            var updateTask = _taskdata.UpdateTask(updatetask);
            return Ok(updateTask);
        }
    }
}
