using DotnetServer.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class ShipperService
    {
        private readonly IMongoCollection<Shipper> _shipperCollection;

        public ShipperService(IDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _shipperCollection = mongoDatabase.GetCollection<Shipper>("Shipper");
        }

        public List<Shipper> Get() =>
             _shipperCollection.Find(_ => true).ToList();

        public Shipper Get(string id) =>
             _shipperCollection.Find(x => x._id == id).FirstOrDefault();

        public Shipper Create(Shipper newShipper)
            {   _shipperCollection.InsertOne(newShipper);
                return newShipper;
            }

            public void Update(string id, Shipper updatedShipper) =>
                 _shipperCollection.ReplaceOne(x => x._id == id, updatedShipper);

            public void Remove(string id) =>
                 _shipperCollection.DeleteOne(x => x._id == id);
    }
}