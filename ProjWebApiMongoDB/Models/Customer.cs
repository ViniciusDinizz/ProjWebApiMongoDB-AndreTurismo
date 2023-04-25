using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjWebApiMongoDB.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Adress { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
