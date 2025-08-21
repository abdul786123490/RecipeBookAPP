using MediatR;
using RecipeBook.Application.DTOs;
using RecipeBook.Application.DTOs.Users;

namespace RecipeBook.Application.Features.Users.Queries;

public class GetAllUsersQuery : IRequest<List<UserDto>>
{
}
