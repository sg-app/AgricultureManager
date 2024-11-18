using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerFeatures
{
    public class GetFertilizerListCommand : IReq<IList<FertilizerVm>>
    {
    }

    public class GetFertilizerListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFertilizerListCommand, IList<FertilizerVm>>
    {
        public async Task<Response<IList<FertilizerVm>>> Handle(GetFertilizerListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Fertilizer
                .AsNoTracking()
                .Include(f => f.FertilizerToDetails)
                .ThenInclude(f => f.FertilizerDetail)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<FertilizerVm>>(entities));
        }
    }
}
