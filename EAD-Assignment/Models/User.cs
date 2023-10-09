using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EAD_Assignment.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public ObjectId Id { get; set; }
        public string NIC { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;


    }
}
