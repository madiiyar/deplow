using Microsoft.AspNetCore.Mvc;
using deployAPI.Services;
using deployAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace deployAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly JsonFileService _jsonFileService;

        public UsersController(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _jsonFileService.GetUsers();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var users = _jsonFileService.GetUsers();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            var users = _jsonFileService.GetUsers();

            // Check if the ID already exists
            if (users.Any(u => u.Id == user.Id))
            {
                return BadRequest(new { Message = $"User with ID {user.Id} already exists." });
            }

            users.Add(user);
            _jsonFileService.SaveUsers(users);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User updatedUser)
        {
            var users = _jsonFileService.GetUsers();
            var userIndex = users.FindIndex(u => u.Id == id);

            if (userIndex == -1)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            users[userIndex] = updatedUser; // Replace the user with updated details
            _jsonFileService.SaveUsers(users);

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var users = _jsonFileService.GetUsers();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found." });
            }

            users.Remove(user);
            _jsonFileService.SaveUsers(users);

            return NoContent();
        }
    }
}
