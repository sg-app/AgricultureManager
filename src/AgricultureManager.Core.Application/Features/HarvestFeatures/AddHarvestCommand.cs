using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.HarvestFeatures
{
    public class AddHarvestCommand : IReq<HarvestVm>
    {
        public Guid HarvestUnitId { get; set; }
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public double Quantity { get; set; }
        public Guid? UnitId { get; set; }
        public string? Setting { get; set; }
        public string? Comment { get; set; }

    }
    public class AddHarvestCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddHarvestCommand, HarvestVm>
    {
        public async Task<Response<HarvestVm>> Handle(AddHarvestCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<Harvest>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.Harvest.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<HarvestVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
