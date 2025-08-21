using MediatR;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Features.Users.Queries;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _tokenService;

    public LoginUserQueryHandler(IUserRepository userRepository, IJwtTokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password");

        using var hmac = new HMACSHA256();
        var enteredHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));

        if (enteredHash != user.PasswordHash)
            throw new UnauthorizedAccessException("Invalid username or password");

        return _tokenService.GenerateToken(user);
    }
}
