namespace RecipeBook.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "User"; // "User" or "Admin"
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
