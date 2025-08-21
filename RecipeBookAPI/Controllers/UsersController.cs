using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.Features.Recipes.Commands;
using RecipeBook.Application.Features.Recipes.Queries;
using RecipeBook.Application.Features.Users.Commands;
using RecipeBook.Application.Features.Users.Queries;
using System.Security.Claims;

namespace RecipeBook.API.Controllers;

[ApiController]
[Route("api")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // -----------------------------
    // USER AUTHENTICATION ENDPOINTS
    // -----------------------------

    /// <summary>
    /// User Registration
    /// POST: /api/users/register
    /// </summary>
    [HttpPost("users/register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// User Login
    /// POST: /api/users/login
    /// </summary>
    [HttpPost("users/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var token = await _mediator.Send(query);
        return Ok(new { Token = token });
    }

    /// <summary>
    /// Get Logged-in User Profile
    /// GET: /api/users/profile
    /// </summary>

    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        var username = User.Identity?.Name;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new
        {
            UserId = userId,
            Username = username,
            Role = role
        });
    }


    // -----------------------------
    // RECIPE MANAGEMENT ENDPOINTS
    // -----------------------------

    /// <summary>
    /// Create a New Recipe
    /// POST: /api/recipes
    /// </summary>
    [HttpPost("CreateRecipes")]
    [Authorize]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get All Recipes
    /// GET: /api/recipes
    /// </summary>
    [HttpGet("GetAllrecipes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllRecipes()
    {
        var result = await _mediator.Send(new GetAllRecipesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get Recipe By ID
    /// GET: /api/recipes/{recipeId}
    /// </summary>
    [HttpGet("recipesById/{recipeId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRecipeById(int recipeId)
    {
        var result = await _mediator.Send(new GetRecipeByIdQuery { RecipeId = recipeId });
        return Ok(result);
    }

    /// <summary>
    /// Update Recipe
    /// PUT: /api/recipes/{recipeId}
    /// </summary>
    [HttpPut("UpdateRecipes/{recipeId}")]
    [Authorize]
    public async Task<IActionResult> UpdateRecipe(int recipeId, [FromBody] UpdateRecipeCommand command)
    {
        command.RecipeId = recipeId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Delete Recipe
    /// DELETE: /api/recipes/{recipeId}
    /// </summary>
    [HttpDelete("DeleteRecipes/{recipeId}")]
    [Authorize]
    public async Task<IActionResult> DeleteRecipe(int recipeId)
    {
        var result = await _mediator.Send(new DeleteRecipeCommand { RecipeId = recipeId });
        return Ok(new { Message = "Recipe deleted successfully" });
    }

    /// <summary>
    /// Search Recipes
    /// GET: /api/recipes/search?keyword=xyz
    /// </summary>
    [HttpGet("recipes/search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchRecipes([FromQuery] string keyword)
    {
        var result = await _mediator.Send(new SearchRecipesQuery { Keyword = keyword });
        return Ok(result);
    }
}
