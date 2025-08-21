using MediatR;
using RecipeBook.Application.DTOs.Categories;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Features.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category { Name = request.Name };
        await _categoryRepository.AddAsync(category);

        return new CategoryDto { CategoryId = category.CategoryId, Name = category.Name };
    }
}
