﻿namespace RecipeBook.Application.DTOs.Users;

public class UserResponseDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}
