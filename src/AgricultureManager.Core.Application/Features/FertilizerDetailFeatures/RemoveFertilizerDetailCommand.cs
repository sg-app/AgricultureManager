using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FertilizerDetailFeatures
{
    public class RemoveFertilizerDetailCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveFertilizerDetailCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFertilizerDetailCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFertilizerDetailCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.FertilizerDetail.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Element nicht gefunden.");

            dbContext.FertilizerDetail.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
