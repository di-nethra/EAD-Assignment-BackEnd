using EAD_Assignment.Models;
using EAD_Assignment.Repositories.UserRepository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EAD_Assignment.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {

        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IConfiguration configuration)
        {

            var connectionString = configuration["MongoDBSettings:ConnectionString"];
            var databaseName = configuration["MongoDBSettings:DatabaseName"];
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _userCollection = database.GetCollection<User>("users");
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var objectId = new ObjectId(userId);
            var filter = Builders<User>.Filter.Eq(u => u.Id, objectId);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetByNICAsync(string nic)
        {
            var filter = Builders<User>.Filter.Eq(u => u.NIC, nic);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }

        //public async Task UpdateUserAsync(User user)
        //{

        //    var objectId = new ObjectId(user.Id);
        //    var filter = Builders<User>.Filter.Eq(u => u.Id, objectId);
        //    await _userCollection.ReplaceOneAsync(filter, user);
        //}

        public async Task DeleteUserAsync(string userId)
        {
            var objectId = new ObjectId(userId);
            var filter = Builders<User>.Filter.Eq(u => u.Id, objectId);
            await _userCollection.DeleteOneAsync(filter);
        }
    }
}
