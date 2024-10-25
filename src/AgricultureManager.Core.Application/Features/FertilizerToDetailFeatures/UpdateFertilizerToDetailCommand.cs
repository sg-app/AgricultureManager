using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerToDetailFeatures
{
    public class UpdateFertilizerToDetailCommand : IReq<FertilizerToDetailVm>
    {
        public Guid FertilizerId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateFertilizerToDetailCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFertilizerToDetailCommand, FertilizerToDetailVm>
    {
        public async Task<Response<FertilizerToDetailVm>> Handle(UpdateFertilizerToDetailCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var fertilizerToDetail = await dbContext.FertilizerToDetail.FindAsync([request.FertilizerDetailId, request.FertilizerId], cancellationToken);

            if (fertilizerToDetail is null)
            {
                return Response.Fail<FertilizerToDetailVm>("Dünger/Detail nicht gefunden.");
            }

            mapper.Map(request, fertilizerToDetail);

            dbContext.FertilizerToDetail.Update(fertilizerToDetail);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fertilizerToDetailVm = mapper.Map<FertilizerToDetailVm>(fertilizerToDetail);
            return Response.Success(fertilizerToDetailVm);
        }
    }
}
