using DotnetServer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetServer.Services
{


    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerService(IDatabaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);

            _customerCollection = mongoDatabase.GetCollection<Customer>("Customer");
        }

        public List<Customer> Get()
        {
            var dummy = _customerCollection.Find(x => true
             ).ToList();
            return dummy;
        }
            
        public Customer GetCusFromPhone(string phone)
        {
            var dummy = _customerCollection.Find(x => x.phone == phone);

            return dummy.FirstOrDefault();
        }


        public Customer Get(string id) =>
             _customerCollection.Find(x => x._id == id).FirstOrDefault();

        public Customer Create(Customer newCustomer)
        {
            var dummy = newCustomer;

            _customerCollection.InsertOne(dummy);
            return newCustomer;
        }

        public Customer Update(string id, Customer updatedCustomer)
        {
            _customerCollection.ReplaceOne(x => x._id == id, updatedCustomer);
            return updatedCustomer;
        }


        public void Remove(string id) =>
             _customerCollection.DeleteOne(x => x._id == id);
    }
}