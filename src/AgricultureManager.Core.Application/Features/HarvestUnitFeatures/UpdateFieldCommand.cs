using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.HarvestUnitFeatures
{
    public class UpdateHarvestUnitCommand : IReq<HarvestUnitVm>
    {
        public Guid Id { get; set; }
        public Guid HarvestYearId { get; set; }
        public Guid FieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        public Guid CultureId { get; set; }
    }

    public class UpdateHarvestUnitCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateHarvestUnitCommand, HarvestUnitVm>
    {
        public async Task<Response<HarvestUnitVm>> Handle(UpdateHarvestUnitCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var havestYear = await dbContext.HarvestUnit.FindAsync([request.Id], cancellationToken);
            if (havestYear is null)
            {
                return Response.Fail<HarvestUnitVm>("Ernteeinheit nicht gefunden.");
            }

            mapper.Map(request, havestYear);

            dbContext.HarvestUnit.Update(havestYear);
            await dbContext.SaveChangesAsync(cancellationToken);

            var harvestYear = mapper.Map<HarvestUnitVm>(havestYear);
            return Response.Success(harvestYear);
        }
    }
}
