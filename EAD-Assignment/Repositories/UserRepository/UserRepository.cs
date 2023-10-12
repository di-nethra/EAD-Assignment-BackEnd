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

        public async Task UpdateUserExceptRoleAsync(UserUpdate updatedUser)
        {
            //// Implement logic to update a user (except for the role) in the MongoDB database
            //var objectId = updatedUser.Id;

            //// Convert the ObjectId to a string
            //string objectIdAsString = objectId.ToString();
            //var newobjectId = new ObjectId(objectIdAsString);
            var filter = Builders<User>.Filter.Eq(u => u.NIC, updatedUser.NIC);

                // Create an update definition to exclude the role field
                var update = Builders<User>.Update
                    .Set(u => u.FirstName, updatedUser.FirstName)
                    .Set(u => u.LastName, updatedUser.LastName)
                    .Set(u => u.PasswordHash, updatedUser.PasswordHash);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            // Implement logic to get users by role from the MongoDB database
            var filter = Builders<User>.Filter.Eq(u => u.Role, role);
            return await _userCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateAccountStatusAsync(string nic, string accountStatus)
        {
            // Implement logic to update the account status in the MongoDB database
            var filter = Builders<User>.Filter.Eq(u => u.NIC, nic);

            // Create an update definition to set the account status
            var update = Builders<User>.Update.Set(u => u.AccountStatus, accountStatus);

            await _userCollection.UpdateOneAsync(filter, update);
        }
    }
}
