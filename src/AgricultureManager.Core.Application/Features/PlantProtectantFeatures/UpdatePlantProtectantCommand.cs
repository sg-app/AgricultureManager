using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.PlantProtectantFeatures
{
    public class UpdatePlantProtectantCommand : IReq<PlantProtectantVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PlantProtectantTypeVm PlantProtectantType { get; set; }
        public string? ActiveSubstance { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdatePlantProtectantCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdatePlantProtectantCommand, PlantProtectantVm>
    {
        public async Task<Response<PlantProtectantVm>> Handle(UpdatePlantProtectantCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var plantProtectant = await dbContext.PlantProtectant.FindAsync([request.Id], cancellationToken);
            if (plantProtectant is null)
            {
                return Response.Fail<PlantProtectantVm>("Pflanzenschutzmittel nicht gefunden.");
            }

            mapper.Map(request, plantProtectant);

            dbContext.PlantProtectant.Update(plantProtectant);
            await dbContext.SaveChangesAsync(cancellationToken);

            var plantProtectantVm = mapper.Map<PlantProtectantVm>(plantProtectant);
            return Response.Success(plantProtectantVm);
        }
    }
}
