using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.PlantProtectantFeatures
{
    public class AddPlantProtectantCommand : IReq<PlantProtectantVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PlantProtectantTypeVm PlantProtectantType { get; set; }
        public string? ActiveSubstance { get; set; }
        public string? Comment { get; set; }
    }
    public class AddPlantProtectantCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddPlantProtectantCommand, PlantProtectantVm>
    {
        public async Task<Response<PlantProtectantVm>> Handle(AddPlantProtectantCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var plantProtectant = mapper.Map<PlantProtectant>(request);
            plantProtectant.Id = Guid.NewGuid();

            await dbContext.PlantProtectant.AddAsync(plantProtectant, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var plantProtectantVm = mapper.Map<PlantProtectantVm>(plantProtectant);
            return Response.Success(plantProtectantVm);
        }
    }
}
