using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KnockoutDemo.Source.Domain.UseCases.DeleteAllUsers
{
    using KnockoutDemo.Entities;

    public class DeleteAllUsersCommand : IRequest
    {
        public class DeleteAllUsersCommandHandler : IRequestHandler<DeleteAllUsersCommand>
        {
            private readonly DataContext context;

            public DeleteAllUsersCommandHandler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(DeleteAllUsersCommand request, CancellationToken cancellationToken)
            {
                context.Users.RemoveRange(context.Users);

                await context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
