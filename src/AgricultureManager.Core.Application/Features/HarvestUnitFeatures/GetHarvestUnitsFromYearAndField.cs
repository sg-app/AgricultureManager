using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public record GetHarvestUnitsFromYearAndField(Guid YearId, Guid FieldId) : IReq<ICollection<HarvestUnitVm>>;

    public class GetHarvestUnitsFromYearAndFieldHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetHarvestUnitsFromYearAndField, ICollection<HarvestUnitVm>>
    {
        public async Task<Response<ICollection<HarvestUnitVm>>> Handle(GetHarvestUnitsFromYearAndField request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.HarvestUnit
                .Include(f => f.Field)
                .Include(f=>f.Culture)
                .Where(f => f.HarvestYearId == request.YearId && f.FieldId == request.FieldId)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<ICollection<HarvestUnitVm>>(entities));

        }
    }
}
