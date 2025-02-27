// Path: Repositories/TaskRepository.cs
using MongoDB.Driver;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskItem> _taskCollection;

        public TaskRepository(MongoDbService mongoDbService)
        {
            var database = mongoDbService.ConnectToDatabase();
            _taskCollection = database.GetCollection<TaskItem>("Tasks");
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _taskCollection.InsertOneAsync(task);
            return task;
        }

        public async Task<TaskItem> GetByIdAsync(string taskId)
        {
            return await _taskCollection.Find(task => task.Id == taskId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _taskCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            await _taskCollection.ReplaceOneAsync(t => t.Id == task.Id, task);
        }

        public async Task DeleteAsync(string taskId)
        {
            await _taskCollection.DeleteOneAsync(task => task.Id == taskId);
        }
    }
}
