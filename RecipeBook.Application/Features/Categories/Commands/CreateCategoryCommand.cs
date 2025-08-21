using MediatR;
using RecipeBook.Application.DTOs.Categories;

namespace RecipeBook.Application.Features.Categories.Commands;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; set; }
}
