using DotnetServer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class ShopService
    {
        private readonly IMongoCollection<Shop> _shopCollection;

        public ShopService(IDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _shopCollection = mongoDatabase.GetCollection<Shop>("Shop");
        }

        public List<Shop> Get()
        {
            var dummy = _shopCollection.Find(x => true
             ).ToList();
            return dummy;
        }
            



        public Shop Get(string id) =>
             _shopCollection.Find(x => x._id == id).FirstOrDefault();

        public Shop Create(Shop newShop)
        {
            var dummy = newShop;

            _shopCollection.InsertOne(dummy);
            return newShop;
        }

        public Shop Update(string id, Shop updatedShop)
        {
            _shopCollection.ReplaceOne(x => x._id == id, updatedShop);
            return updatedShop;
        }


        public void Remove(string id) =>
             _shopCollection.DeleteOne(x => x._id == id);
    }
}