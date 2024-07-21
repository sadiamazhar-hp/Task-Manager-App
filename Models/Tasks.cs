using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TaskManager.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }

    public class Tasks
    {
       // [BsonId]
       // [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; } 

       // [BsonElement("Title")]
        public string Title { get; set; }

       // [BsonElement("Description")]
        public string Description { get; set; }

       // [BsonElement("DueDate")]
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DueDate { get; set; } 

        // [BsonElement("Assigned")]
        //[BsonRepresentation(BsonType.Int32)]
         public int Assigned { get; set; } //Assigned is the User ID

        //[BsonElement("Priority")]
        //[BsonRepresentation(BsonType.String)]
        public Priority Priority { get; set; }

        //[BsonElement("Status")]
        //[BsonRepresentation(BsonType.String)]
        public Status Status { get; set; }
    }
}
