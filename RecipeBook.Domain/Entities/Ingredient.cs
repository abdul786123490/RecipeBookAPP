namespace RecipeBook.Domain.Entities;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
