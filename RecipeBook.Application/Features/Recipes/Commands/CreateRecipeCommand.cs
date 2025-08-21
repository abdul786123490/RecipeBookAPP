using MediatR;
using RecipeBook.Application.DTOs.Recipe;
using RecipeBook.Application.DTOs.Recipes;

namespace RecipeBook.Application.Features.Recipes.Commands;

public class CreateRecipeCommand : IRequest<RecipeDto>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<int> IngredientIds { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<int> DietaryIds { get; set; }
}
