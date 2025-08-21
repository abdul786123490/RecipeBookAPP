// Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RecipeBookAPI.Application.Services;
using RecipeBookAPI.Core.Interfaces;
using RecipeBookAPI.Infrastructure.Data;
using RecipeBookAPI.Infrastructure.Repositories;
using RecipeBookAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


// Register DbContext with SQL Server connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDietaryInformationRepository, DietaryInformationRepository>();
builder.Services.AddScoped<IJwtService, JwtService>(); // Register the JwtService
builder.Services.AddScoped<IJwtService, JwtService>();

// Register other services (e.g., RecipeService, UserService)
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
