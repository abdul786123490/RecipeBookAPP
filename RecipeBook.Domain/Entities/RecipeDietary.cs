namespace RecipeBook.Domain.Entities;

public class RecipeDietary
{
    public int RecipeDietaryId { get; set; }
    public int RecipeId { get; set; }
    public int DietaryId { get; set; }

    public Recipe Recipe { get; set; } = default!;
    public DietaryInfo Dietary { get; set; } = default!;
}
