using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectantStore
{
    public record LoadPlantProtectantsDataAction();
    public record LoadPlantProtectantsDataResultAction(IEnumerable<PlantProtectantVm> PlantProtectants);
    public record LoadPlantProtectantDataResultFailAction();
    public record AddPlantProtectantAction(PlantProtectantVm PlantProtectant);
    public record UpdatePlantProtectantAction(PlantProtectantVm PlantProtectant);
    public record RemovePlantProtectantAction(Guid PlantProtectantId);
}
