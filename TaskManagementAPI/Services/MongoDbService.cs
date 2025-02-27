using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class MongoDbService
    {
        private readonly MongoDbSettings _mongoDbSettings;

        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings.Value;
        }

        // Connects to MongoDB and retrieves the database
        public IMongoDatabase ConnectToDatabase()
        {
            var client = new MongoClient(_mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
            return database;
        }

        // A simple test function to ensure the connection works
        public void TestConnection()
        {
            var client = new MongoClient(_mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
            var collectionNames = database.ListCollectionNames().ToList();
            Console.WriteLine($"Connected to MongoDB. Collections: {string.Join(", ", collectionNames)}");
        }
    }
}
