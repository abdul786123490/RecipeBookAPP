using MediatR;
using RecipeBook.Application.DTOs.Users;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if username exists
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            throw new Exception("Username already exists");

        // Create user directly without hashing
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.Password,  // Save plain text password
            Role = string.IsNullOrEmpty(request.Role) ? "User" : request.Role
        };

        await _userRepository.AddAsync(user);

        return new UserResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Role = user.Role
        };
    }
}
