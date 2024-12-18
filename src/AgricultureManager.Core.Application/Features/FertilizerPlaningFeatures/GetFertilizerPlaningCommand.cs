using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures
{
    public record GetFertilizerPlaningCommand(Guid HarvestUnitId) : IReq<ICollection<FertilizerPlaningVm>>
    {
    }

    public class GetFertilizerPlaningCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFertilizerPlaningCommand, ICollection<FertilizerPlaningVm>>
    {
        public async Task<Response<ICollection<FertilizerPlaningVm>>> Handle(GetFertilizerPlaningCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.FertilizerPlaning
                .AsNoTracking()
                .Where(x => x.HarvestUnitId == request.HarvestUnitId)
                .OrderBy(o => o.Order)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<ICollection<FertilizerPlaningVm>>(entities));
        }
    }
}
