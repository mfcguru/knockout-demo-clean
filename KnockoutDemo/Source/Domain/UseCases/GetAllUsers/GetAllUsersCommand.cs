using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnockoutDemo.Source.Domain.UseCases.GetAllUsers
{
    using KnockoutDemo.Entities;    

    public class GetAllUsersCommand : IRequest<List<GetAllUsersResultDto>>
    {
        public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, List<GetAllUsersResultDto>>
        {
            private readonly DataContext context;

            public GetAllUsersCommandHandler(DataContext context) => this.context = context;

            public async Task<List<GetAllUsersResultDto>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
            {
                var result = await context.Users
                    .OrderBy(o => o.UserId)
                    .Select(o => new GetAllUsersResultDto
                    {
                        UserId = o.UserId,
                        FullName = string.Format("{0} {1}", o.FirstName, o.LastName),
                        Age = o.Age
                    }).ToListAsync();

                return result;
            }
        }
    }
}
