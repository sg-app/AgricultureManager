using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.HarvestFeatures
{
    public record RemoveHarvestCommand(Guid Id) : IReq { }

    public class RemoveHarvestCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveHarvestCommand>
    {
        public async Task<ResponseLess> Handle(RemoveHarvestCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Harvest.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Ernte nicht gefunden.");

            dbContext.Harvest.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
