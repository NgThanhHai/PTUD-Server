using DotnetServer.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class ExtenInforService
    {
        private readonly IMongoCollection<ExtenInfor> _ExtenInforCollection;

        public ExtenInforService(IDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _ExtenInforCollection = mongoDatabase.GetCollection<ExtenInfor>("ExtenInfor");
        }

        public List<ExtenInfor> Get() =>
             _ExtenInforCollection.Find(_ => true).ToList();

        public ExtenInfor Get(string id) =>
             _ExtenInforCollection.Find(x => x._id == id).FirstOrDefault();

        public ExtenInfor Create(ExtenInfor newExtenInfor)
        {
            _ExtenInforCollection.InsertOne(newExtenInfor);
            return newExtenInfor;
        }

        public void Update(string id, ExtenInfor updatedExtenInfor) =>
             _ExtenInforCollection.ReplaceOne(x => x._id == id, updatedExtenInfor);

        public void Remove(string id) =>
             _ExtenInforCollection.DeleteOne(x => x._id == id);
    }
}