using EAD_Assignment.Models;
using System.Threading.Tasks;

namespace EAD_Assignment.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByNICAsync(string nic);
        Task<User> GetByIdAsync(string userId);
        Task CreateUserAsync(User user);
        Task<User> AuthenticateAsync(string nic, string password);
        Task UpdateUserExceptRoleAsync(UserUpdate updatedUser);
        Task<List<User>> GetUsersByRoleAsync(string role);
        Task EnableAccountStatusAsync(string nic, string accountStatus);
        Task DisableAccountStatusAsync(string nic, string accountStatus);
    }
}
