using MediatR;
using RecipeBook.Application.Abstractions;

namespace RecipeBook.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null) return false;

            category.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
