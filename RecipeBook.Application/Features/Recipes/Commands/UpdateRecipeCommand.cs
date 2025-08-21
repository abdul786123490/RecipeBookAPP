using MediatR;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Recipes.Commands;

public class UpdateRecipeCommand : IRequest<Recipe>
{
    public int RecipeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int> IngredientIds { get; set; } = new();
    public List<int> CategoryIds { get; set; } = new();
    public List<int> DietaryIds { get; set; } = new();
}
