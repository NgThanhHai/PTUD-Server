using DotnetServer.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class TroubleService
    {
        private readonly IMongoCollection<Trouble> _TroubleCollection;

        public TroubleService(IDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _TroubleCollection = mongoDatabase.GetCollection<Trouble>("Trouble");
        }

        public List<Trouble> Get() =>
             _TroubleCollection.Find(_ => true).ToList();

        public Trouble Get(string id) =>
             _TroubleCollection.Find(x => x._id == id).FirstOrDefault();

        public Trouble Create(Trouble newTrouble)
        {
            _TroubleCollection.InsertOne(newTrouble);
            return newTrouble;
        }

        public void Update(string id, Trouble updatedTrouble) =>
             _TroubleCollection.ReplaceOne(x => x._id == id, updatedTrouble);

        public void Remove(string id) =>
             _TroubleCollection.DeleteOne(x => x._id == id);
    }
}