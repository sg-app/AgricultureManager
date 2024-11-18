using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.HarvestYearFeatures
{
    public class AddHarvestYearCommand() : IReq<HarvestYearVm>
    {
        public string Year { get; set; } = string.Empty;
    }
    public class AddHarvestYearCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddHarvestYearCommand, HarvestYearVm>
    {
        public async Task<Response<HarvestYearVm>> Handle(AddHarvestYearCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var harvestYear = mapper.Map<HarvestYear>(request);
            harvestYear.Id = Guid.NewGuid();

            await dbContext.HarvestYear.AddAsync(harvestYear, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<HarvestYearVm>(harvestYear);
            return Response.Success(fieldVm);
        }
    }
}
