using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FertilizerToDetailFeatures
{
    public class RemoveFertilizerToDetailCommand : IReq
    {
        public Guid FertilizerId { get; set; }
        public Guid FertilizerDetailId { get; set; }
    }

    public class RemoveFertilizerToDetailCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFertilizerToDetailCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFertilizerToDetailCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.FertilizerToDetail.FindAsync([request.FertilizerDetailId, request.FertilizerId], cancellationToken);
            if (entity is null)
                return Response.Fail("Dünger/Detail nicht gefunden.");

            dbContext.FertilizerToDetail.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
