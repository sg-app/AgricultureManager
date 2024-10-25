using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerToDetailFeatures
{
    public class AddFertilizerToDetailCommand : IReq<FertilizerToDetailVm>
    {
        public Guid FertilizerId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }
    }
    public class AddFertilizerToDetailCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFertilizerToDetailCommand, FertilizerToDetailVm>
    {
        public async Task<Response<FertilizerToDetailVm>> Handle(AddFertilizerToDetailCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var fertilizerToDetail = mapper.Map<FertilizerToDetail>(request);
            

            await dbContext.FertilizerToDetail.AddAsync(fertilizerToDetail, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fertilizerToDetailVm = mapper.Map<FertilizerToDetailVm>(fertilizerToDetail);
            return Response.Success(fertilizerToDetailVm);
        }
    }
}
