// API/Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using RecipeBookAPI.Core.Entities;
using RecipeBookAPI.Application.Services;

//using RecipeBookAPI.Application.UseCases;

namespace RecipeBookAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await _userService.RegisterUserAsync(user);
            return StatusCode(201);  // Created
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var token = await _userService.LoginUserAsync(user);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
