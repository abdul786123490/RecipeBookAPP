using MediatR;
using RecipeBook.Application.DTOs.Users;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

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

        // Hash password
        using var hmac = new HMACSHA256();
        var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));

        // Create user
        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            Role = "User"
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
