using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.HarvestFeatures
{
    public class UpdateHarvestCommand : IReq<HarvestVm>
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        public string? Setting { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateHarvestCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateHarvestCommand, HarvestVm>
    {
        public async Task<Response<HarvestVm>> Handle(UpdateHarvestCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.Harvest.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<HarvestVm>("Ernte nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.Harvest.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<HarvestVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
