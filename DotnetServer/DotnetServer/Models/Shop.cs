using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DotnetServer.Models
{

    public class Shop
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string business_cert { get; set; }

    }

}
