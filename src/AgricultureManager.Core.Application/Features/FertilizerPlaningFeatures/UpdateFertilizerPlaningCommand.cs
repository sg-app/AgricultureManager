using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures
{
    public class UpdateFertilizerPlaningCommand : IReq<FertilizerPlaningVm>
    {
        public Guid Id { get; set; }
        public Guid HarvestUnitId { get; set; }
        public int Order { get; set; }
        public Guid FertilizerId { get; set; }
        public float Dosage { get; set; }
        public Guid? UnitId { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateFertilizerPlaningCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFertilizerPlaningCommand, FertilizerPlaningVm>
    {
        public async Task<Response<FertilizerPlaningVm>> Handle(UpdateFertilizerPlaningCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var field = await dbContext.FertilizerPlaning.FindAsync([request.Id], cancellationToken);
            if (field is null)
            {
                return Response.Fail<FertilizerPlaningVm>("Eintrag nicht gefunden.");
            }

            mapper.Map(request, field);

            dbContext.FertilizerPlaning.Update(field);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<FertilizerPlaningVm>(field);
            return Response.Success(fieldVm);
        }
    }
}
