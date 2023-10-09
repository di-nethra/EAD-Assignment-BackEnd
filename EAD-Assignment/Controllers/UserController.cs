using EAD_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EAD_Assignment.Services.TokenService;
using EAD_Assignment.Services.TokenService.Interfaces;
using EAD_Assignment.Services.UserServices.Interfaces;

namespace EAD_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            try
            {
                // Validate input
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the NIC is already registered
                var existingUser = await _userService.GetByNICAsync(model.NIC);
                if (existingUser != null)
                {
                    return BadRequest("User with this NIC already exists.");
                }


                // Create a new User object
                var user = new User
                {
                    NIC = model.NIC,
                    PasswordHash = model.PasswordHash,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = model.Role,
                    AccountStatus = "active" // Default to active when creating an account
                };

                // Create the user in the database (implement in IUserService)
                await _userService.CreateUserAsync(user);

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            try
            {
                // Validate input
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Authenticate the user (implement in IUserService)
                var user = await _userService.AuthenticateAsync(model.NIC, model.PasswordHash);

                if (user == null)
                {
                    return Unauthorized("Invalid NIC or password.");
                }

                // Generate a JWT token
                var token = _tokenService.GenerateToken(user);

                // Return the token in the response
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
