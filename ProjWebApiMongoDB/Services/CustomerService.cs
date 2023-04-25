using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjWebApiMongoDB.Config;
using ProjWebApiMongoDB.Models;

namespace ProjWebApiMongoDB.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customer;
        private readonly IMongoCollection<Address> _address;
        public CustomerService(IProjWebApiMongoDBSettings settings)
        {
            var customer = new MongoClient(settings.ConnectionString);
            var database = customer.GetDatabase(settings.DatabaseName);
            _customer = database.GetCollection<Customer>(settings.CustomerCollectionName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }
        public List<Customer> Get() => _customer.Find(c => true).ToList();
        public Customer Get(string id) => _customer.Find(c => c.Id == id).FirstOrDefault();
        public Customer Create(Customer customer)
        {
            customer.Id = BsonObjectId.GenerateNewId().ToString();
            var addressId = _address.Find(a =>  a.Id == customer.Adress.Id.ToString()).FirstOrDefault();
            customer.Adress = addressId;
            if(addressId == null)
            {
                return customer;
            }
            _customer.InsertOne(customer);
            return customer;
        }
        public void Update (string id, Customer customer) => _customer.ReplaceOne(c => c.Id == id, customer);
        public void Delete(string id) => _customer.DeleteOne(c => c.Id == id);

    }
}
