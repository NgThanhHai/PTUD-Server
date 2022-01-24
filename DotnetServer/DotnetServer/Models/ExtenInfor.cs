using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DotnetServer.Models
{
    public class ExtenInfor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string shipper_id { get; set; }
        public string temp { get; set; }
        public string status { get; set; }
        public string rating { get; set; }

    }
}
