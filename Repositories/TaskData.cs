using TaskManager.Interface;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskData: ITaskRepo
    {
        private List<Tasks> _tasks = new()
        {
           new Tasks {Id =0,Title="UI", Description ="Build UI Wireframe", Assigned=2,
               DueDate= new DateTime(2023, 6, 26, 14, 30, 0), Priority=Priority.Low, Status=Status.Pending },
           new Tasks {Id=1, Title="Setup", Description="Setup All the dependencies to start website Project", Assigned=1,
               DueDate =new DateTime(2023, 3, 2, 12, 34,0),Priority=Priority.Medium, Status=Status.InProgress}

        };

        public IEnumerable<Tasks> GetTasks()
        {
            return (_tasks);
        }

        public Tasks AddTask(Tasks task)
        {
            _tasks.Add(task);
            return task;
        }

        public Tasks RemoveTask(int id) {
            var task = _tasks[id];
            _tasks.Remove(task);
            return task;
        }

        public Tasks  UpdateTask(Tasks task) {
          
            Tasks taskforupdate =_tasks.First(t => t.Id == task.Id);
            if (taskforupdate != null) {
                taskforupdate.Assigned = task.Assigned;
                taskforupdate.Title = task.Title;
                taskforupdate.Description = task.Description;
                taskforupdate.Status = task.Status;
                taskforupdate.Priority = task.Priority;
                taskforupdate.DueDate = task.DueDate;
            }
            var taske =_tasks[task.Id] = taskforupdate;
            return (taske);
        }

        public IEnumerable<Tasks> GetTaskByTitle(string Title)
        {
            return from t in _tasks
                   where (string.IsNullOrEmpty(Title) || t.Title == Title)
                   orderby t.Title
                   select t;
        }
    }



}
