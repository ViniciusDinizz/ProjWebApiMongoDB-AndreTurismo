namespace ProjWebApiMongoDB.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Adress { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
