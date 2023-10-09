using EAD_Assignment.Models;
using System.Threading.Tasks;

namespace EAD_Assignment.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByNICAsync(string nic);
        Task CreateUserAsync(User user);
        Task<User> AuthenticateAsync(string nic, string password);
    }
}
