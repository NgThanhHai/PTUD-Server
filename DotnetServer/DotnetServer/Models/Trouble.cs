using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DotnetServer.Models
{
    public class Trouble
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string order_id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string shipper_id { get; set; }
     
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public string description { get; set; }

        public int status { get; set; }
<<<<<<< Updated upstream
=======

        public string picture { get; set; }
>>>>>>> Stashed changes
    }
}
