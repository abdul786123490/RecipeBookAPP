using MediatR;
using RecipeBook.Application.DTOs.Users;

namespace RecipeBook.Application.Features.Users.Queries;

public class LoginUserQuery : IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
