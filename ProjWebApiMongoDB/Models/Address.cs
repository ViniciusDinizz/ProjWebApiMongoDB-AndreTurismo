using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjWebApiMongoDB.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string BorHood { get; set; }
        public string Cep { get; set; }
        public string Complement { get; set; }
        public City City { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
