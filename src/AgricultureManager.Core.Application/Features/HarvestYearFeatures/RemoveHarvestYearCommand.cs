using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.HarvestYearFeatures
{
    public class RemoveHarvestYearCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveHarvestYearCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveHarvestYearCommand>
    {
        public async Task<ResponseLess> Handle(RemoveHarvestYearCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.HarvestYear.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Erntejahr nicht gefunden.");

            dbContext.HarvestYear.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
