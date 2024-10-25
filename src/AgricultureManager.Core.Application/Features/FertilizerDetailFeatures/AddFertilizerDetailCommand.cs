using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerDetailFeatures
{
    public class AddFertilizerDetailCommand : IReq<FertilizerDetailVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
    public class AddFertilizerDetailCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFertilizerDetailCommand, FertilizerDetailVm>
    {
        public async Task<Response<FertilizerDetailVm>> Handle(AddFertilizerDetailCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var dertilizerDetail = mapper.Map<FertilizerDetail>(request);
            dertilizerDetail.Id = Guid.NewGuid();

            await dbContext.FertilizerDetail.AddAsync(dertilizerDetail, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fertilizerDetailVm = mapper.Map<FertilizerDetailVm>(dertilizerDetail);
            return Response.Success(fertilizerDetailVm);
        }
    }
}
