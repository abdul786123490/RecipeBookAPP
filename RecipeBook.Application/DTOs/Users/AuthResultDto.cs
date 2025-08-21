namespace RecipeBook.Application.DTOs.Users;

public class AuthResultDto
{
    public string Token { get; set; } = default!;
    public DateTime ExpiresAtUtc { get; set; }
}
