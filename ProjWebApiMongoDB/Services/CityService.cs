using MongoDB.Bson;
using MongoDB.Driver;
using ProjWebApiMongoDB.Config;
using ProjWebApiMongoDB.Models;

namespace ProjWebApiMongoDB.Services
{
    public class CityService
    {
        private readonly IMongoCollection<City> _city;
        public CityService(IProjWebApiMongoDBSettings settings)
        {
            var city = new MongoClient(settings.ConnectionString);
            var database = city.GetDatabase(settings.DatabaseName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }
        public List<City> Get() => _city.Find(c => true).ToList();
        public City Get(string id) => _city.Find(c =>  c.Id == id).FirstOrDefault();
        public City Create(City city)
        {
            city.Id = BsonObjectId.GenerateNewId().ToString();
            _city.InsertOne(city);
            return city;
        }
        public void Update(string id, City city) => _city.ReplaceOne(c => c.Id == id, city);
        public void Delete(string id) => _city.DeleteOne(c => c.Id == id);
    }
}
