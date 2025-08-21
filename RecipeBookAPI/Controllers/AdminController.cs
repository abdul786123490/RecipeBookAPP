using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.Features.Users.Queries;
using RecipeBook.Application.Features.Users.Commands;
using RecipeBook.Application.Features.Categories.Commands;
using RecipeBook.Application.Features.Categories.Queries;

namespace RecipeBook.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "admin")] // Only Admins can access these endpoints
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // -----------------------------
        // USER MANAGEMENT (ADMIN ONLY)
        // -----------------------------

        /// <summary>
        /// Get All Users (Admin Only)
        /// GET: /api/admin/users
        /// </summary>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get User by ID (Admin Only)
        /// GET: /api/admin/users/{userId}
        /// </summary>
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { UserId = userId });
            if (result == null)
                return NotFound(new { Message = "User not found" });
            return Ok(result);
        }

        /// <summary>
        /// Update User (Admin Only)
        /// PUT: /api/admin/users/{userId}
        /// </summary>
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserCommand command)
        {
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete User (Admin Only)
        /// DELETE: /api/admin/users/{userId}
        /// </summary>
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = userId });
            return Ok(new { Message = "User deleted successfully" });
        }

        // -----------------------------
        // CATEGORY MANAGEMENT (ADMIN ONLY)
        // -----------------------------

        /// <summary>
        /// Create Category (Admin Only)
        /// POST: /api/admin/categories
        /// </summary>
        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get All Categories (Admin Only)
        /// GET: /api/admin/categories
        /// </summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Update Category (Admin Only)
        /// PUT: /api/admin/categories/{categoryId}
        /// </summary>
        [HttpPut("categories/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryCommand command)
        {
            command.CategoryId = categoryId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete Category (Admin Only)
        /// DELETE: /api/admin/categories/{categoryId}
        /// </summary>
        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _mediator.Send(new DeleteCategoryCommand { CategoryId = categoryId });
            return Ok(new { Message = "Category deleted successfully" });
        }
    }
}
