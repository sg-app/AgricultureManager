using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.HarvestYearFeatures
{
    public record GetHarvestYearsCommand : IReq<IList<HarvestYearVm>>
    {
    }

    public class GetHarvestYearsCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetHarvestYearsCommand, IList<HarvestYearVm>>
    {
        public async Task<Response<IList<HarvestYearVm>>> Handle(GetHarvestYearsCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var list = await dbContext.HarvestYear
                .AsNoTracking()
                .OrderByDescending(f => f.Year)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<HarvestYearVm>>(list));
        }
    }
}
