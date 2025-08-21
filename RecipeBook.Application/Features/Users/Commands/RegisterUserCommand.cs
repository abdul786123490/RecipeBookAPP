using MediatR;
using RecipeBook.Application.DTOs.Users;

namespace RecipeBook.Application.Features.Users.Commands;

public class RegisterUserCommand : IRequest<UserResponseDto>
{

    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User"; // Default is User

}

