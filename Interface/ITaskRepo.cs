using TaskManager.Models;

namespace TaskManager.Interface
{
    public interface ITaskRepo
    {
        public IEnumerable<Tasks> GetTasks();
        public Tasks AddTask(Tasks task);
        public Tasks RemoveTask(int id);
        public Tasks UpdateTask(Tasks task);
        public IEnumerable<Tasks> GetTaskByTitle(string Title);
    }
}
