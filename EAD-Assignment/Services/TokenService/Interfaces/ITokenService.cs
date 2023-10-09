using EAD_Assignment.Models;
using System.Security.Claims;

namespace EAD_Assignment.Services.TokenService.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token, out ClaimsPrincipal principal);
    }
}
