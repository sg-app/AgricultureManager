using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.SeedFeatures
{
    public record GetSeedListCommand(Guid HarvestUnitId) : IReq<IList<SeedVm>> { }


    public class GetSeedListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetSeedListCommand, IList<SeedVm>>
    {
        public async Task<Response<IList<SeedVm>>> Handle(GetSeedListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Seed
                .AsNoTracking()
                .Where(f => f.HarvestUnitId == request.HarvestUnitId)
                .Include(s => s.SeedCategory)
                .Include(s => s.SeedTechnology)
                .Include(s => s.Unit)
                .Include(s => s.Person)
                .Include(s => s.Culture)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<SeedVm>>(entities));
        }
    }
}
