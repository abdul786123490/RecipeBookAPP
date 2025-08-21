using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
