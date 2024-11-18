using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerDetailFeatures
{
    public class UpdateFertilizerDetailCommand : IReq<FertilizerDetailVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }

    public class UpdateFertilizerDetailCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFertilizerDetailCommand, FertilizerDetailVm>
    {
        public async Task<Response<FertilizerDetailVm>> Handle(UpdateFertilizerDetailCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var fertilizerDetail = await dbContext.FertilizerDetail.FindAsync([request.Id], cancellationToken);
            if (fertilizerDetail is null)
            {
                return Response.Fail<FertilizerDetailVm>("Element nicht gefunden.");
            }

            mapper.Map(request, fertilizerDetail);

            dbContext.FertilizerDetail.Update(fertilizerDetail);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fertilizerDetailVm = mapper.Map<FertilizerDetailVm>(fertilizerDetail);
            return Response.Success(fertilizerDetailVm);
        }
    }
}
