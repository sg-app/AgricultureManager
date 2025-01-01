using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public record CheckHaveHarvestUnitsFromYearCommand(Guid YearId) : IReq<bool> { }

    public class CheckHaveHarvestUnitsFromYearCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<CheckHaveHarvestUnitsFromYearCommand, bool>
    {
        public async Task<Response<bool>> Handle(CheckHaveHarvestUnitsFromYearCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.HarvestUnit
                .Where(f => f.HarvestYearId == request.YearId)
                .CountAsync(cancellationToken);

            return Response.Success(entities > 0);
        }
    }
}
