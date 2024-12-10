using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public record RemoveHarvestUnitCommand(Guid Id) : IReq { }

    public class RemoveHarvestUnitCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveHarvestUnitCommand>
    {
        public async Task<ResponseLess> Handle(RemoveHarvestUnitCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.HarvestUnit.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Ernteeinehit nicht gefunden.");

            dbContext.HarvestUnit.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
