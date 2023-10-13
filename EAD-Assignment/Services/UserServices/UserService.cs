using EAD_Assignment.Models;
using EAD_Assignment.Repositories.UserRepository.Interfaces;
using EAD_Assignment.Services.UserServices.Interfaces;

namespace EAD_Assignment.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByNICAsync(string nic)
        {
            // Implement logic to retrieve a user by NIC from your MongoDB database
            return await _userRepository.GetByNICAsync(nic);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            // Implement logic to get a user by ID from the repository
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task CreateUserAsync(User user)
        {
            // Hash the password using BCrypt before storing it
            user.PasswordHash = HashPassword(user.PasswordHash);

            // Implement logic to create a new user in your MongoDB database
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> AuthenticateAsync(string nic, string password)
        {
            // Retrieve the user by NIC from the database
            var user = await _userRepository.GetByNICAsync(nic);

            if (user == null)
            {
                // User with the given NIC does not exist
                return null;
            }

            // Verify the password using BCrypt
            if (VerifyPassword(password, user.PasswordHash))
            {
                // Password is correct, return the user
                return user;
            }

            // Password is incorrect
            return null;
        }

        public async Task UpdateUserExceptRoleAsync(UserUpdate updatedUser)
        {
            // Implement logic to update a user (except for the role) in the repository
            updatedUser.PasswordHash = HashPassword(updatedUser.PasswordHash);
            await _userRepository.UpdateUserExceptRoleAsync(updatedUser);
        }

        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            // Implement logic to get users by role from the repository
            return await _userRepository.GetUsersByRoleAsync(role);
        }

        public async Task EnableAccountStatusAsync(string nic, string accountStatus)
        {
            // Implement logic to update the account status in the repository
            await _userRepository.UpdateAccountStatusAsync(nic, accountStatus);
        }

        public async Task DisableAccountStatusAsync(string nic, string accountStatus)
        {
            // Implement logic to update the account status in the repository
            await _userRepository.UpdateAccountStatusAsync(nic, accountStatus);
        }

        // Implement BCrypt password hashing logic
        private string HashPassword(string password)
        {
            // Generate a salt and hash the password using BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        // Implement BCrypt password verification logic
        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
