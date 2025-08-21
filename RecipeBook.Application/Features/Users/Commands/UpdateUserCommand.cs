using MediatR;
using RecipeBook.Application.Abstractions;

namespace RecipeBook.Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null) return false;

            user.Username = request.Username;
            user.Role = request.Role;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
