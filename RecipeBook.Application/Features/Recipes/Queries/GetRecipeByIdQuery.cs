using MediatR;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Recipes.Queries;

public class GetRecipeByIdQuery : IRequest<Recipe>
{
    public int RecipeId { get; set; }
}
