using EAD_Assignment.Models;

namespace EAD_Assignment.Repositories.UserRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string userId);
        Task<User> GetByNICAsync(string nic);
        Task CreateUserAsync(User user);
        //Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
    }
}
