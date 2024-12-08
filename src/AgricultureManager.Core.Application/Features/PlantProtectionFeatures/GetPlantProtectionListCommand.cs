using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.PlantProtectionFeatures
{
    public record GetPlantProtectionListCommand(Guid HarvestUnitId) : IReq<IList<PlantProtectionVm>> { }


    public class GetPlantProtectionListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetPlantProtectionListCommand, IList<PlantProtectionVm>>
    {
        public async Task<Response<IList<PlantProtectionVm>>> Handle(GetPlantProtectionListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.PlantProtection
                .AsNoTracking()
                .Where(f => f.HarvestUnitId == request.HarvestUnitId)
                .Include(s => s.PlantProtectant)
                .Include(s => s.Unit)
                .Include(s => s.Person)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<PlantProtectionVm>>(entities));
        }
    }
}
