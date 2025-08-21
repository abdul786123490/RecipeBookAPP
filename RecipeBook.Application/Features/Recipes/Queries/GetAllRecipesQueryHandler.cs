using MediatR;
using RecipeBook.Application.DTOs.Recipe;
using RecipeBook.Application.Interfaces.Repositories;

namespace RecipeBook.Application.Features.Recipes.Queries;

public class GetAllRecipesQueryHandler : IRequestHandler<GetAllRecipesQuery, List<RecipeDto>>
{
    private readonly IRecipeRepository _recipeRepository;

    public GetAllRecipesQueryHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<List<RecipeDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await _recipeRepository.GetAllAsync();

        return recipes.Select(r => new RecipeDto
        {
            RecipeId = r.RecipeId,
            Title = r.Title,
            Description = r.Description,
            CreatedAt = r.CreatedAt,
            CreatedBy = r.UserId.ToString()
        }).ToList();
    }
}
