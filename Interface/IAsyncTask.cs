using TaskManager.Models;

namespace TaskManager.Interface
{
    public interface IAsyncTask
    {
        Task<IEnumerable<Tasks>> GetAllTasksAsync();
        Task<IEnumerable<Tasks>> GetTaskByIdAsync(int id);
        Task<Tasks> UpdateTaskAsync(Tasks task);
        Task<Tasks> CreateTaskAsync(Tasks task);
        Task<Tasks> DeleteTaskAsync(int id);
    }
}
