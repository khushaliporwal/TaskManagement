using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDbSettings:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Connects to MongoDB and retrieves the database
        public IMongoDatabase ConnectToDatabase()
        {
            return _database;
        }

        // A simple test function to ensure the connection works
        public void TestConnection()
        {
            var collectionNames = _database.ListCollectionNames().ToList();
            Console.WriteLine($"Connected to MongoDB. Collections: {string.Join(", ", collectionNames)}");
        }
    }
}
