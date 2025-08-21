//using RecipeBook.Domain.Entities;

//namespace RecipeBook.Application.Interfaces.Repositories;

//public interface IRecipeRepository
//{
//    Task<Recipe?> GetByIdAsync(int id);
//    Task<IEnumerable<Recipe>> GetAllAsync();
//    Task<IEnumerable<Recipe>> SearchAsync(string keyword);
//    Task AddAsync(Recipe recipe);
//    Task UpdateAsync(Recipe recipe);
//    Task DeleteAsync(Recipe recipe);
//}



using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Interfaces.Repositories;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<List<Recipe>> SearchAsync(string keyword);
    Task AddAsync(Recipe recipe);
    Task UpdateAsync(Recipe recipe);
    Task DeleteAsync(Recipe recipe);
}
