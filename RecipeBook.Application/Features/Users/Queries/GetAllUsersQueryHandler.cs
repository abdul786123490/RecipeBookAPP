using MediatR;
using RecipeBook.Application.DTOs.Categories;
using RecipeBook.Application.DTOs.Users;
using RecipeBook.Application.Interfaces.Repositories;

namespace RecipeBook.Application.Features.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // Fetch all users from repository
        var users = await _userRepository.GetAllAsync();

        // Convert to DTOs
        return users.Select(u => new UserDto
        {
            Id = u.UserId,
            Username = u.Username,
            Role = u.Role
        }).ToList();
    }
}
