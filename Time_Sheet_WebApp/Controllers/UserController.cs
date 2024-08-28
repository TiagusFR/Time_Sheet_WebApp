using Microsoft.AspNetCore.Mvc;
using Time_Sheet_WebApp.Controllers.Requests;
using Time_Sheet_WebApp.Entities;
using Time_Sheet_WebApp.Services;

namespace Time_Sheet_WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService 
            ?? throw new ArgumentNullException(nameof(userService));

        // Get all users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        // Get user by ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Create a new user
        [HttpPost]
        public IActionResult Post(UserRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                IsDeleted = false
            };

            var result = _userService.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // Update an existing user
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateUserRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }

            var existingUser = _userService.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Pass the request to the Update method in the IUserService
            _userService.Update(id, request);
            return NoContent();
        }


        // Delete (soft delete) a user
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userService.Delete(id);
            return NoContent();
        }
    }
}
