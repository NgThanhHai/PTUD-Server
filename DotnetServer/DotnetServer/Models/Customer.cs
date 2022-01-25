using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DotnetServer.Models
{

    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string name { get; set; }
        [BsonDateTimeOptions]
        public DateTime? birth { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string identify { get; set; }
        public string sex { get; set; }
        public string? otp { get; set; }

    }

}
