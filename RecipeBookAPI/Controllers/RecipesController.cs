//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using RecipeBook.Application.Features.Recipes.Commands;
//using RecipeBook.Application.Features.Recipes.Queries;

//namespace RecipeBook.API.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class RecipesController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public RecipesController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    [HttpPost("CreateRecipe/")]
//    //[Authorize]
//    public async Task<IActionResult> Create([FromBody] CreateRecipeCommand command)
//    {
//        var result = await _mediator.Send(command);
//        return Ok(result);
//    }

//    [HttpGet("GetAllRecipe")]
//    [AllowAnonymous]
//    public async Task<IActionResult> GetAll()
//    {
//        var result = await _mediator.Send(new GetAllRecipesQuery());
//        return Ok(result);
//    }

//    [HttpGet("search")]
//    [AllowAnonymous]
//    public async Task<IActionResult> Search([FromQuery] string keyword)
//    {
//        var result = await _mediator.Send(new SearchRecipesQuery { Keyword = keyword });
//        return Ok(result);
//    }
//}
