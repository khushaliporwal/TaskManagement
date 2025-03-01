using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }  // MongoDB _id field

    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? CompletedAt { get; set; } // Nullable for tasks not completed yet
}
