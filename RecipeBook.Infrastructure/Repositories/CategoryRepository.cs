using Microsoft.EntityFrameworkCore;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;
using RecipeBook.Infrastructure;

namespace RecipeBook.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly RecipeBookDbContext _context;

    public CategoryRepository(RecipeBookDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(int id) =>
        await _context.Categories.FindAsync(id);

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await _context.Categories.ToListAsync();

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
