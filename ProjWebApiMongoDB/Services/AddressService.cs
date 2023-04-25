using MongoDB.Driver;
using ProjWebApiMongoDB.Config;
using ProjWebApiMongoDB.Controllers;
using ProjWebApiMongoDB.Models;

namespace ProjWebApiMongoDB.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _address;
        private readonly IMongoCollection<City> _city;
        public AddressService(IProjWebApiMongoDBSettings settings)
        {
            var city = new MongoClient(settings.ConnectionString);
            var database = city.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }
        public List<Address> Get() => _address.Find(a => true).ToList();
        public Address Get(string id) => _address.Find(a => a.Id == id).FirstOrDefault();
        public Address Create(Address address)
        {
            var city = address.City;
            var existcity = _city.Find(ci => ci.Description == city.Description).FirstOrDefault();

            if (existcity == null)
            {
                _city.InsertOne(city);
                existcity = _city.Find(ci => ci.Description == city.Description).FirstOrDefault();
            }
            address.City = existcity;
            _address.InsertOne(address);
            return address;
        }
        public void Update(string id, Address address) => _address.ReplaceOne(a => a.Id == id, address);
        public void Delete(string id) => _address.DeleteOne(a => a.Id == id);
    }
}
