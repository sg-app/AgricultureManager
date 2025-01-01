using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizationFeatures
{
    public class AddFertilizationCommand : IReq<FertilizationVm>
    {
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
    public class AddFertilizationCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFertilizationCommand, FertilizationVm>
    {
        public async Task<Response<FertilizationVm>> Handle(AddFertilizationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var culture = mapper.Map<Fertilization>(request);
            culture.Id = Guid.NewGuid();

            await dbContext.Fertilization.AddAsync(culture, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var cultureVm = mapper.Map<FertilizationVm>(culture);
            return Response.Success(cultureVm);
        }
    }
}
