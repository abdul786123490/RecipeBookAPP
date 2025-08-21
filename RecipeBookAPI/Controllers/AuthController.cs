//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using RecipeBook.Application.Features.Users.Commands;
//using RecipeBook.Application.Features.Users.Queries;
//using RecipeBook.Infrastructure.Services;


//namespace RecipeBook.API.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class AuthController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public AuthController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    [HttpPost("register")]
//    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
//    {
//        var result = await _mediator.Send(command);
//        return Ok(result);
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
//    {
//        var token = await _mediator.Send(query);
//        return Ok(new { token });
//    }
//}
