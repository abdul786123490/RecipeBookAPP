using MediatR;
using RecipeBook.Application.DTOs.Recipe;
using RecipeBook.Application.Interfaces.Repositories;

namespace RecipeBook.Application.Features.Recipes.Queries;

public class SearchRecipesQueryHandler : IRequestHandler<SearchRecipesQuery, List<RecipeDto>>
{
    private readonly IRecipeRepository _recipeRepository;

    public SearchRecipesQueryHandler(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<List<RecipeDto>> Handle(SearchRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await _recipeRepository.SearchAsync(request.Keyword);

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
