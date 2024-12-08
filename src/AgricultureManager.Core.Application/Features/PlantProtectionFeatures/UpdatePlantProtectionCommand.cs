using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.PlantProtectionFeatures
{
    public class UpdatePlantProtectionCommand : IReq<PlantProtectionVm>
    {
        public Guid Id { get; set; }
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

    public class UpdatePlantProtectionCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdatePlantProtectionCommand, PlantProtectionVm>
    {
        public async Task<Response<PlantProtectionVm>> Handle(UpdatePlantProtectionCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.PlantProtection.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<PlantProtectionVm>("Pflanzenschutz nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.PlantProtection.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<PlantProtectionVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
