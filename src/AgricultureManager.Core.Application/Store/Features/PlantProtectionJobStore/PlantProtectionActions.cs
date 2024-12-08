using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectionJobStore
{
    public record LoadPlantProtectionJobDataAction(Guid HarvestUnitId) : ILoadDataAction { }
    public record LoadPlantProtectionJobDataResultAction(IEnumerable<PlantProtectionVm> PlantProtections);
    public record LoadPlantProtectionJobDataResultFailAction();
    public record AddPlantProtectionJobAction(PlantProtectionVm PlantProtection);
    public record UpdatePlantProtectionJobAction(PlantProtectionVm PlantProtection);
    public record RemovePlantProtectionJobAction(Guid PlantProtectionId);

}
