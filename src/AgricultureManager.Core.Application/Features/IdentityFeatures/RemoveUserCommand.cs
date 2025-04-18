using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.IdentityFeatures
{
    public record RemoveUserCommand(string UserName) : IReq;

    public class RemoveUserCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveUserCommand>
    {
        public async Task<ResponseLess> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.HarvestYear.FindAsync([request.UserName], cancellationToken);
            if (entity is null)
                return Response.Fail("Benutzer nicht gefunden.");

            dbContext.HarvestYear.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
