using EAD_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EAD_Assignment.Services.TokenService;
using EAD_Assignment.Services.TokenService.Interfaces;
using EAD_Assignment.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Login([FromBody] UserLogin model)
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

                if (user.AccountStatus != "active")
                {
                    return Unauthorized("Account is diabled. Contact Backoffice Agent for activation");
                }

                // Generate a JWT token
                //var token = _tokenService.GenerateToken(user);

                // Return the token in the response
                //return Ok(new { Token = token });

                // Return the user in the response
                return Ok(new { User = user });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("get-by-nic/{nic}")]
        public async Task<IActionResult> GetUserByNIC(string nic)
        {
            try
            {
                var user = await _userService.GetByNICAsync(nic);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserByNIC(UserUpdate updatedUser)
        {
            try
            {

                // Update the user (except for the role)
                await _userService.UpdateUserExceptRoleAsync(updatedUser);

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("travellers")]
        public async Task<IActionResult> GetTravellerList()
        {
            try
            {
                var travellers = await _userService.GetUsersByRoleAsync("Traveller");

                return Ok(travellers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("travelAgents")]
        public async Task<IActionResult> GetTravelAgentList()
        {
            try
            {
                var travelleAgents = await _userService.GetUsersByRoleAsync("TravelAgent");

                return Ok(travelleAgents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("activate-account/{nic}")]
        public async Task<IActionResult> ActivateTravellerAccount(string nic)
        {
            try
            {
                await _userService.EnableAccountStatusAsync(nic, "active");

                return Ok("Traveller account activated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("disable-account/{nic}")]
        public async Task<IActionResult> DisableTravellerAccount(string nic)
        {
            try
            {
                await _userService.DisableAccountStatusAsync(nic, "in-active");

                return Ok("Traveller account de-activated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
