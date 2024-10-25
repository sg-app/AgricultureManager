using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerToDetailFeatures
{
    public class GetFertilizerToDetailListCommand : IReq<IList<FertilizerToDetailVm>>
    {
    }

    public class GetFertilizerToDetailListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFertilizerToDetailListCommand, IList<FertilizerToDetailVm>>
    {
        public async Task<Response<IList<FertilizerToDetailVm>>> Handle(GetFertilizerToDetailListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.FertilizerToDetail
                .AsNoTracking()
                .Include(f=>f.FertilizerDetail)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<FertilizerToDetailVm>>(entities));
        }
    }
}
