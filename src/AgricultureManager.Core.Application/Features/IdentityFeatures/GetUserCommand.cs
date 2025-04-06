using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Application.Shared.Models.Identity;

namespace AgricultureManager.Core.Application.Features.IdentityFeatures
{
    public record GetUserCommand(string Username) : IReq<UserVm>    {    }
    
    public class GetUserCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<GetUserCommand, UserVm>
    {
        public async Task<Response<UserVm>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.User.FindAsync([request.Username ], cancellationToken);
            if (entity is null)
            {
                return Response.Fail<UserVm>("Benutzer nicht gefunden.");
            }
            var user = new UserVm
            {
                UserName = entity.UserName,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
            };
            return Response.Success(user);
        }
    }
}
