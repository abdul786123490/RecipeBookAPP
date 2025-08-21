using MediatR;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Recipes.Queries;

public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, Recipe>
{
    private readonly IRecipeRepository _recipeRepository;

    public GetRecipeByIdQueryHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<Recipe> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _recipeRepository.GetByIdAsync(request.RecipeId);
        if (recipe == null)
            throw new KeyNotFoundException("Recipe not found.");

        return recipe;
    }
}
