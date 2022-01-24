using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetServer.Models
{

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }


    }
}
