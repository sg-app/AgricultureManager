using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestFeatures
{
    public record GetHarvestListCommand(Guid HarvestUnitId) : IReq<IList<HarvestVm>> { }


    public class GetHarvestListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetHarvestListCommand, IList<HarvestVm>>
    {
        public async Task<Response<IList<HarvestVm>>> Handle(GetHarvestListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Harvest
                .AsNoTracking()
                .Where(f => f.HarvestUnitId == request.HarvestUnitId)
                .Include(s => s.Unit)
                .Include(s => s.Person)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<HarvestVm>>(entities));
        }
    }
}
