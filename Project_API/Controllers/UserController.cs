using _Services.Contracts;
using _Services.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // Get all users
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpGet("GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // Create a new user
        [HttpPost]
        public IActionResult CreateUser([FromBody] User_Create _user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.CreateUser(_user);
            return Ok("User created successfully.");
        }

        // Update an existing user
        [HttpPut("UpdateById/{id}")]
        public IActionResult UpdateById(int id, [FromBody] User_Update _user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.UpdateUser(id, _user);
            return Ok("User updated successfully.");
        }

        [HttpPut("UpdateByEmail/{email}")]
        public IActionResult UpdateByEmail(string email, [FromBody] User_Update _user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.UpdateUser(email, _user);
            return Ok("User updated successfully.");
        }

        // Delete a user by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }

        [HttpDelete("{email}")]
        public IActionResult DeleteUser(string email)
        {
            _userService.DeleteUser(email);
            return Ok("User deleted successfully.");
        }

        // Authenticate user by email and password
        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser(string email, string password)
        {
            try
            {
                var user = _userService.AuthenticateUser(email, password);
                return Ok(user);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password.");
            }
        }

        // Update user's password
        [HttpPut("{userId}/password")]
        public IActionResult UpdateUserPassword(int userId, [FromBody] string newPassword)
        {
            _userService.UpdateUserPassword(userId, newPassword);
            return Ok("Password updated successfully.");
        }

        // Get user's properties
        [HttpGet("{userId}/properties")]
        public IActionResult GetUserProperties(int userId)
        {
            var properties = _userService.GetUserProperties(userId);
            return Ok(properties);
        }

        // Get user's favorites
        [HttpGet("{userId}/favorites")]
        public IActionResult GetUserFavorites(int userId)
        {
            var favorites = _userService.GetUserFavoritesProperty(userId);
            return Ok(favorites);
        }
    }
}
