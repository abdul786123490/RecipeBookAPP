//using Microsoft.EntityFrameworkCore;
//using RecipeBook.Application.Interfaces.Repositories;
//using RecipeBook.Domain.Entities;
//using RecipeBook.Infrastructure;

//namespace RecipeBook.Infrastructure.Repositories;

//public class RecipeRepository : IRecipeRepository
//{
//    private readonly RecipeBookDbContext _context;

//    public RecipeRepository(RecipeBookDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<Recipe?> GetByIdAsync(int id) =>
//        await _context.Recipes
//            .Include(r => r.User)
//            .FirstOrDefaultAsync(r => r.RecipeId == id);

//    public async Task<IEnumerable<Recipe>> GetAllAsync() =>
//        await _context.Recipes.Include(r => r.User).ToListAsync();

//    public async Task<IEnumerable<Recipe>> SearchAsync(string keyword) =>
//        await _context.Recipes
//            .Where(r => r.Title.Contains(keyword) || r.Description.Contains(keyword))
//            .ToListAsync();

//    public async Task AddAsync(Recipe recipe)
//    {
//        await _context.Recipes.AddAsync(recipe);
//        await _context.SaveChangesAsync();
//    }

//    public async Task UpdateAsync(Recipe recipe)
//    {
//        _context.Recipes.Update(recipe);
//        await _context.SaveChangesAsync();
//    }

//    public async Task DeleteAsync(Recipe recipe)
//    {
//        _context.Recipes.Remove(recipe);
//        await _context.SaveChangesAsync();
//    }
//}



using Microsoft.EntityFrameworkCore;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;
using RecipeBook.Infrastructure;

namespace RecipeBook.Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeBookDbContext _context;

    public RecipeRepository(RecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        return await _context.Recipes.ToListAsync();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        return await _context.Recipes.FindAsync(id);
    }

    public async Task<List<Recipe>> SearchAsync(string keyword)
    {
        return await _context.Recipes
            .Where(r => r.Title.Contains(keyword) || r.Description.Contains(keyword))
            .ToListAsync();
    }

    public async Task AddAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Recipe recipe)
    {
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }
}
