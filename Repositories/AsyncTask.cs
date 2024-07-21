using System.Threading.Tasks;
using TaskManager.Interface;
using TaskManager.Models;
namespace TaskManager.Repositories

{
    public class AsyncTask : IAsyncTask
    {
        private List<Tasks> _tasks = new()
        {
           new Tasks {Id =0,Title="UI/UX", Description ="Build UI Wireframe Website", Assigned=2,
               DueDate= new DateTime(2023, 6, 26, 14, 30, 0), Priority=Priority.Low, Status=Status.Pending },
           new Tasks {Id=1, Title="SetupOfProject", Description="Setup All the dependencies", Assigned=1,
               DueDate =new DateTime(2023, 3, 2, 12, 34,0),Priority=Priority.Medium, Status=Status.InProgress}

        };
        public async Task<Tasks> CreateTaskAsync(Tasks task)
        {
            await Task.Delay(100);
            _tasks.Add(task);
            return task;

        }


        public async Task<Tasks> DeleteTaskAsync(int id)
        {
            await Task.Delay(100);
            var task = _tasks[id];
            _tasks.Remove(task);
            return task;
        }

        public async Task<IEnumerable<Tasks>> GetAllTasksAsync()
        {
            await Task.Delay(100);
            return _tasks;
        }

        public async Task<IEnumerable<Tasks>> GetTaskByIdAsync(int id)
        {
            
            var tasks = _tasks.Where(task => task.Assigned == id);
            return tasks;
        }

        public async Task<Tasks> UpdateTaskAsync(Tasks task)
        {
            Tasks taskforupdate = _tasks.First(t => t.Id == task.Id);
            if (taskforupdate != null)
            {
                taskforupdate.Assigned = task.Assigned;
                taskforupdate.Title = task.Title;
                taskforupdate.Description = task.Description;
                taskforupdate.Status = task.Status;
                taskforupdate.Priority = task.Priority;
                taskforupdate.DueDate = task.DueDate;
            }
            var taske = _tasks[task.Id] = taskforupdate;
            return (taske);
            
        }
    }

}
