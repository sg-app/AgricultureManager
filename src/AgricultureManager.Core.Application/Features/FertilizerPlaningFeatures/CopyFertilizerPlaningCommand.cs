using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures
{
    public record CopyFertilizerPlaningCommand(
        IEnumerable<Guid> HarvestUnitIds,
        IEnumerable<FertilizerPlaningVm> FertilizerPlanings,
        IEnumerable<FertilizerPlaningSpecificationVm> FertilizerPlaningSpecifications) : IReq
    { }

    public class CopyFertilizerPlaningCommandHandler(IAppDbContextFactory contextFactory, IMapper mapper) : IReqHandler<CopyFertilizerPlaningCommand>
    {
        public async Task<ResponseLess> Handle(CopyFertilizerPlaningCommand request, CancellationToken cancellationToken)
        {
            using var context = contextFactory.CreateDbContext();

            foreach (var harvestUnitId in request.HarvestUnitIds)
            {
                var planingCount = await context.FertilizerPlaning
                    .Where(x => x.HarvestUnitId == harvestUnitId)
                    .CountAsync(cancellationToken);
                if (planingCount != 0)
                {
                    var copyPlaningData = mapper.Map<IList<FertilizerPlaning>>(request.FertilizerPlanings);
                    for (int i = 0; i < copyPlaningData.Count; i++)
                    {
                        copyPlaningData[i].HarvestUnitId = harvestUnitId;
                    }
                    await context.FertilizerPlaning.AddRangeAsync(copyPlaningData, cancellationToken);
                }

                var planingSpecCount = await context.FertilizerPlaningSpecification
                    .Where(x => x.HarvestUnitId == harvestUnitId)
                    .CountAsync(cancellationToken);
                if (planingSpecCount != 0)
                {
                    var copySpecData = mapper.Map<IList<FertilizerPlaningSpecification>>(request.FertilizerPlaningSpecifications);
                    for (int i = 0; i < copySpecData.Count; i++)
                    {
                        copySpecData[i].HarvestUnitId = harvestUnitId;
                    }
                    await context.FertilizerPlaningSpecification.AddRangeAsync(copySpecData, cancellationToken);
                }
            }
            await context.SaveChangesAsync(cancellationToken);

            return Response.Success();
        }
    }
}
