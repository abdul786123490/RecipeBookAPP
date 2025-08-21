namespace RecipeBook.Domain.Entities;

public class RecipeIngredient
{
    public int RecipeIngredientId { get; set; }
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public string Quantity { get; set; } = "1 unit";

    public Recipe Recipe { get; set; } = default!;
    public Ingredient Ingredient { get; set; } = default!;
}
