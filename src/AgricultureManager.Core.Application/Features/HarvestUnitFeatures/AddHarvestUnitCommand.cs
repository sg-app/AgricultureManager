using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public class AddHarvestUnitCommand : IReq<HarvestUnitVm>
    {
        public Guid HarvestYearId { get; set; }
        public Guid FieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        public Guid CultureId { get; set; }
    }
    public class AddHarvestUnitCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddHarvestUnitCommand, HarvestUnitVm>
    {
        public async Task<Response<HarvestUnitVm>> Handle(AddHarvestUnitCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var harvestUnit = mapper.Map<HarvestUnit>(request);
            harvestUnit.Id = Guid.NewGuid();

            await dbContext.HarvestUnit.AddAsync(harvestUnit, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var harvestUnitVm = mapper.Map<HarvestUnitVm>(harvestUnit);
            return Response.Success(harvestUnitVm);
        }
    }
}
