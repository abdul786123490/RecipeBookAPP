using MediatR;
using RecipeBook.Application.DTOs.Recipe;

namespace RecipeBook.Application.Features.Recipes.Queries;

public class GetAllRecipesQuery : IRequest<List<RecipeDto>> { }
