using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizationFeatures
{
    public class UpdateFertilizationCommand : IReq<FertilizationVm>
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public DateTime Date { get; set; }
        public Guid? PersonId { get; set; }
        public Guid FertilizerId { get; set; }
        public double Dosage { get; set; }
        public Guid? UnitId { get; set; }
        public string? BBCH { get; set; }
        public string? Setting { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateFertilizationCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFertilizationCommand, FertilizationVm>
    {
        public async Task<Response<FertilizationVm>> Handle(UpdateFertilizationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = await dbContext.Fertilization.FindAsync([request.Id], cancellationToken);
            if (culture is null)
            {
                return Response.Fail<FertilizationVm>("Düngung nicht gefunden.");
            }

            mapper.Map(request, culture);

            dbContext.Fertilization.Update(culture);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<FertilizationVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
