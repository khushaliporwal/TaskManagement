// Path: Services/TaskService.cs
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<TaskItem> CreateTask(TaskItem task)
        {
            return _taskRepository.CreateAsync(task);
        }

        public Task<TaskItem> GetTaskById(string taskId)
        {
            return _taskRepository.GetByIdAsync(taskId);
        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _taskRepository.GetAllAsync().Result;  // Using .Result here for simplicity, but async/await is better
        }

        public Task UpdateTask(TaskItem task)
        {
            return _taskRepository.UpdateAsync(task);
        }

        public Task DeleteTask(string taskId)
        {
            return _taskRepository.DeleteAsync(taskId);
        }
    }
}
