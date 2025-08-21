namespace RecipeBook.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
}
