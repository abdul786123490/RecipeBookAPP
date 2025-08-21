using MediatR;

namespace RecipeBook.Application.Features.Recipes.Commands;

public class DeleteRecipeCommand : IRequest<bool>
{
    public int RecipeId { get; set; }
}
