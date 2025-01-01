using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures
{
    public class AddFertilizerPlaningCommand : IReq<FertilizerPlaningVm>
    {
        public Guid HarvestUnitId { get; set; }
        public int Order { get; set; }
        public Guid FertilizerId { get; set; }
        public float Dosage { get; set; }
        public Guid? UnitId { get; set; }
        public string? Comment { get; set; }
    }
    public class AddFertilizerPlaningCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFertilizerPlaningCommand, FertilizerPlaningVm>
    {
        public async Task<Response<FertilizerPlaningVm>> Handle(AddFertilizerPlaningCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var field = mapper.Map<FertilizerPlaning>(request);
            field.Id = Guid.NewGuid();

            await dbContext.FertilizerPlaning.AddAsync(field, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<FertilizerPlaningVm>(field);
            return Response.Success(fieldVm);
        }
    }
}
