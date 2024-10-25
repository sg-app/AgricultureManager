using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.SeedTechnologyFeatures
{
    public class GetSeedTechnologyListCommand : IReq<IList<SeedTechnologyVm>>
    {
    }

    public class GetSeedTechnologyListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetSeedTechnologyListCommand, IList<SeedTechnologyVm>>
    {
        public async Task<Response<IList<SeedTechnologyVm>>> Handle(GetSeedTechnologyListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.SeedTechnology.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<SeedTechnologyVm>>(entities));
        }
    }
}
