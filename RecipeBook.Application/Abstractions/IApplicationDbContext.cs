using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using System.Collections.Generic;

namespace RecipeBook.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Recipe> Recipes { get; }
    DbSet<Ingredient> Ingredients { get; }
    DbSet<Category> Categories { get; }
    DbSet<DietaryInfo> DietaryInformation { get; }
    DbSet<RecipeIngredient> RecipeIngredients { get; }
    DbSet<RecipeCategory> RecipeCategories { get; }
    DbSet<RecipeDietary> RecipeDietaries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
