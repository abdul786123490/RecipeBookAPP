using MediatR;
using RecipeBook.Application.Interfaces.Repositories;

namespace RecipeBook.Application.Features.Recipes.Commands;

public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, bool>
{
    private readonly IRecipeRepository _recipeRepository;

    public DeleteRecipeCommandHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _recipeRepository.GetByIdAsync(request.RecipeId);
        if (recipe == null)
            throw new KeyNotFoundException("Recipe not found.");

        await _recipeRepository.DeleteAsync(recipe);
        return true;
    }
}
