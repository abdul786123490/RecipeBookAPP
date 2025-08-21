using MediatR;
using RecipeBook.Application.DTOs.Recipe;

namespace RecipeBook.Application.Features.Recipes.Queries;

public class SearchRecipesQuery : IRequest<List<RecipeDto>>
{
    public string Keyword { get; set; }
}
