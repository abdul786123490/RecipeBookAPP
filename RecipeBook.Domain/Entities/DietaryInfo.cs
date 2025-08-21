using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Domain.Entities;

public class DietaryInfo
{
    [Key]
    public int DietaryId { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<RecipeDietary> RecipeDietaries { get; set; } = new List<RecipeDietary>();
}
