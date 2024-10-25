using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerDetailFeatures
{
    public class GetFertilizerDetailListCommand : IReq<IList<FertilizerDetailVm>>
    {
    }

    public class GetFertilizerDetailListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFertilizerDetailListCommand, IList<FertilizerDetailVm>>
    {
        public async Task<Response<IList<FertilizerDetailVm>>> Handle(GetFertilizerDetailListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.FertilizerDetail.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<FertilizerDetailVm>>(entities));
        }
    }
}
