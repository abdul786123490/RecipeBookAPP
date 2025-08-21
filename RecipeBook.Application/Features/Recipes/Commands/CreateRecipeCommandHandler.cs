using MediatR;
using RecipeBook.Application.DTOs.Recipe;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Recipes.Commands;

public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
{
    private readonly IRecipeRepository _recipeRepository;

    public CreateRecipeCommandHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            Title = request.Title,
            Description = request.Description,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _recipeRepository.AddAsync(recipe);

        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            Title = recipe.Title,
            Description = recipe.Description,
            CreatedAt = recipe.CreatedAt,
            CreatedBy = recipe.UserId.ToString()
        };
    }
}
