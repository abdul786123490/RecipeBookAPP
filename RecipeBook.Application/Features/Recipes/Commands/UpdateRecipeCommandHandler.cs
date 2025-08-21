using MediatR;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Recipes.Commands;

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, Recipe>
{
    private readonly IRecipeRepository _recipeRepository;

    public UpdateRecipeCommandHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<Recipe> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _recipeRepository.GetByIdAsync(request.RecipeId);
        if (recipe == null)
            throw new KeyNotFoundException("Recipe not found.");

        recipe.Title = request.Title;
        recipe.Description = request.Description;

        await _recipeRepository.UpdateAsync(recipe);

        return recipe;
    }
}
