using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetServer.Models
{

    public class Shipper
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } 
        public string name { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string negative_cert { get; set; }
        public string vaccine_cert { get; set; }
        public string identify { get; set; }



    }
}
