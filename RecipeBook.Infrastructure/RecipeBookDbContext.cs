using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Reflection.Emit;

namespace RecipeBook.Infrastructure;

public class RecipeBookDbContext : DbContext
{
    public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<DietaryInfo> DietaryInformation { get; set; }
    public DbSet<RecipeDietary> RecipeDietaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User → Recipes (1:N)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Recipes)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // RecipeIngredient junction
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId);

        // RecipeCategory junction
        modelBuilder.Entity<RecipeCategory>()
            .HasOne(rc => rc.Recipe)
            .WithMany(r => r.RecipeCategories)
            .HasForeignKey(rc => rc.RecipeId);

        modelBuilder.Entity<RecipeCategory>()
            .HasOne(rc => rc.Category)
            .WithMany(c => c.RecipeCategories)
            .HasForeignKey(rc => rc.CategoryId);

        // RecipeDietary junction
        modelBuilder.Entity<RecipeDietary>()
            .HasOne(rd => rd.Recipe)
            .WithMany(r => r.RecipeDietaries)
            .HasForeignKey(rd => rd.RecipeId);

        modelBuilder.Entity<RecipeDietary>()
            .HasOne(rd => rd.Dietary)
            .WithMany(d => d.RecipeDietaries)
            .HasForeignKey(rd => rd.DietaryId);
    }
}
