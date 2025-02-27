// Path: Repositories/ITaskRepository.cs
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateAsync(TaskItem task); // Returns the created task
        Task<TaskItem> GetByIdAsync(string taskId); // Returns the task by its ID
        Task<IEnumerable<TaskItem>> GetAllAsync(); // Returns all tasks
        Task UpdateAsync(TaskItem task); // Updates an existing task
        Task DeleteAsync(string taskId); // Deletes a task by ID
    }
}
