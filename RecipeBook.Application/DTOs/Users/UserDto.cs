namespace RecipeBook.Application.DTOs.Users;

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Role { get; set; } = "User";
}
