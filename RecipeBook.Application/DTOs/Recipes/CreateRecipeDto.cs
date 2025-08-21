namespace RecipeBook.Application.DTOs.Recipes;

public class CreateRecipeDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<int> IngredientIds { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<int> DietaryIds { get; set; }
}
