using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizationFeatures
{
    public record GetFertilizationListCommand(Guid HarvestUnitId) : IReq<IList<FertilizationVm>> { }


    public class GetFertilizationListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFertilizationListCommand, IList<FertilizationVm>>
    {
        public async Task<Response<IList<FertilizationVm>>> Handle(GetFertilizationListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Fertilization
                .AsNoTracking()
                .Where(f => f.HarvestUnitId == request.HarvestUnitId)
                .Include(s => s.Fertilizer)
                .Include(s => s.Unit)
                .Include(s => s.Person)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<FertilizationVm>>(entities));
        }
    }
}
