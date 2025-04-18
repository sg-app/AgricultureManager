using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.IdentityFeatures
{
    public record GetUsersCommand() : IReq<IEnumerable<UserVm>>    {    }
    
    public class GetUsersCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<GetUsersCommand, IEnumerable<UserVm>>
    {
        public async Task<Response<IEnumerable<UserVm>>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var list = await dbContext.User
                .AsNoTracking()
                .Select(x => new UserVm
                {
                    UserName = x.UserName,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                })
                .ToListAsync(cancellationToken);

            return Response.Success<IEnumerable<UserVm>>(list);
        }
    }
}
