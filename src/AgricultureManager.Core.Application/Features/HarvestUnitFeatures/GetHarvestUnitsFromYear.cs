using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public record GetHarvestUnitsFromYear(Guid YearId) : IReq<List<HarvestUnitOverview>>;

    public class GetHarvestUnitsFromYearHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetHarvestUnitsFromYear, List<HarvestUnitOverview>>
    {
        public async Task<Response<List<HarvestUnitOverview>>> Handle(GetHarvestUnitsFromYear request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.HarvestUnit
                .Where(f => f.HarvestYearId == request.YearId)
                .Include(f => f.Field)
                .Include(i => i.Culture)
                .Select(s => new HarvestUnitOverview
                {
                    Id = s.Id,
                    HarvestUnitName = s.Name,
                    FieldName = s.Field.Name,
                    Area = s.Area,
                    CultureShortName = s.Culture.ShortName ?? "TBD",
                    HarvestUnit = mapper.Map<HarvestUnitVm>(s)
                })
                .ToListAsync(cancellationToken);

            return Response.Success(entities);

        }
    }
}
