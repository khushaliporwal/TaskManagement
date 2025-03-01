// Path: Repositories/TaskRepository.cs
using MongoDB.Bson;
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
            var database = mongoDbService.ConnectToDatabase(); // Use GetDatabase() method instead of ConnectToDatabase()
            _taskCollection = database.GetCollection<TaskItem>("Tasks");
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Id))
            {
                task.Id = ObjectId.GenerateNewId().ToString(); // Ensure valid MongoDB ObjectId
            }

            await _taskCollection.InsertOneAsync(task);
            return task;
        }

        public async Task<TaskItem?> GetByIdAsync(string taskId)
        {
            if (!ObjectId.TryParse(taskId, out ObjectId objectId))
                return null; // Invalid ID format, return null

            return await _taskCollection.Find(task => task.Id == objectId.ToString()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _taskCollection.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            if (!ObjectId.TryParse(task.Id, out ObjectId objectId))
                return false; // Invalid ID format, update fails

            var result = await _taskCollection.ReplaceOneAsync(t => t.Id == objectId.ToString(), task);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string taskId)
        {
            if (!ObjectId.TryParse(taskId, out ObjectId objectId))
                return false; // Invalid ID format, deletion fails

            var result = await _taskCollection.DeleteOneAsync(task => task.Id == objectId.ToString());
            return result.DeletedCount > 0;
        }
    }
}
