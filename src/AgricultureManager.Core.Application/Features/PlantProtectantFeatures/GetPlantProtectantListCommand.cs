using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.PlantProtectantFeatures
{
    public class GetPlantProtectantListCommand : IReq<IList<PlantProtectantVm>>
    {
    }

    public class GetPlantProtectantListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetPlantProtectantListCommand, IList<PlantProtectantVm>>
    {
        public async Task<Response<IList<PlantProtectantVm>>> Handle(GetPlantProtectantListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.PlantProtectant.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<PlantProtectantVm>>(entities));
        }
    }
}
