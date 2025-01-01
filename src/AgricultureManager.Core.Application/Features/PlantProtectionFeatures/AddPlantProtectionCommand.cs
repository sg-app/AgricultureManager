using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.PlantProtectionFeatures
{
    public class AddPlantProtectionCommand : IReq<PlantProtectionVm>
    {
        public Guid HarvestUnitId { get; set; }
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid PlantProtectantId { get; set; }
        public double Dosage { get; set; }
        public Guid? UnitId { get; set; }
        public string? BBCH { get; set; }
        public string? Setting { get; set; }
        public string? Comment { get; set; }
    }
    public class AddPlantProtectionCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddPlantProtectionCommand, PlantProtectionVm>
    {
        public async Task<Response<PlantProtectionVm>> Handle(AddPlantProtectionCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<PlantProtection>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.PlantProtection.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<PlantProtectionVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
