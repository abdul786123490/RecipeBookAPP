//using MediatR;
//using RecipeBook.Application.Interfaces.Repositories;
//using RecipeBook.Application.Interfaces.Services;

//namespace RecipeBook.Application.Features.Users.Queries;

//public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
//{
//    private readonly IUserRepository _userRepository;
//    private readonly IJwtTokenService _tokenService;

//    public LoginUserQueryHandler(IUserRepository userRepository, IJwtTokenService tokenService)
//    {
//        _userRepository = userRepository;
//        _tokenService = tokenService;
//    }

//    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
//    {
//        // Find user by username
//        var user = await _userRepository.GetByUsernameAsync(request.Username);
//        if (user == null)
//            throw new UnauthorizedAccessException("Invalid username or password");

//        // Compare plain text passwords directly
//        if (user.PasswordHash != request.Password)
//            throw new UnauthorizedAccessException("Invalid username or password");

//        // Generate JWT token
//        return _tokenService.GenerateToken(user);
//    }
//}



using MediatR;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Application.Interfaces.Services;

namespace RecipeBook.Application.Features.Users.Queries
{
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

            if (user == null || user.PasswordHash != request.Password) // ✅ Using plain password
                throw new UnauthorizedAccessException("Invalid username or password");

            // Generate JWT Token with Role included
            return _tokenService.GenerateToken(user);
        }
    }
}


