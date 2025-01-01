using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public record TransferHarvestUnitsFromLastYearCommand(Guid YearId) : IReq;

    public class TransferHarvestUnitsFromLastYearCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<TransferHarvestUnitsFromLastYearCommand>
    {
        public async Task<ResponseLess> Handle(TransferHarvestUnitsFromLastYearCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var years = await dbContext.HarvestYear
                .OrderBy(y => y.Year)
                .ToListAsync(cancellationToken);

            var currentYear = await dbContext.HarvestYear
                .Where(y => y.Id == request.YearId)
                .FirstOrDefaultAsync(cancellationToken);

            if (currentYear == null)
            {
                return Response.Fail("Aktuelles Jahr nicht gefunden.");
            }

            var previousYear = years
                .TakeWhile(y => y != currentYear)
                .LastOrDefault();

            if (previousYear == null)
            {
                return Response.Fail("Vorgängerjahr nicht gefunden.");
            }

            await dbContext.HarvestUnit
                .Where(f => f.HarvestYearId == previousYear.Id)
                .ForEachAsync(hu =>
                {
                    var toAdd = mapper.Map<HarvestUnit>(hu);
                    hu.Id = Guid.NewGuid();
                    hu.HarvestYearId = currentYear.Id;
                    dbContext.Add(toAdd);
                }, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Success();

        }
    }
}
