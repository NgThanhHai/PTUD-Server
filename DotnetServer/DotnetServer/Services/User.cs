using DotnetServer.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>("User");
        }

        public List<User> Get() =>
             _userCollection.Find(_ => true).ToList();

        public User Get(string id) =>
             _userCollection.Find(x => x._id == id).FirstOrDefault();
        public User Get(string username, string password) =>
             _userCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefault();

        public User Create(User newUser)
        {
            _userCollection.InsertOne(newUser);
            return newUser;
        }

        public User Update(string id, User updatedUser)
        {
            _userCollection.ReplaceOne(x => x._id == id, updatedUser);
            return updatedUser;
        }


        public void Remove(string id) =>
             _userCollection.DeleteOne(x => x._id == id);
    }
}