using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.PlantProtectantStore
{
    public record LoadPlantProtectantsDataAction();
    public record LoadPlantProtectantsDataResultAction(IEnumerable<PlantProtectantVm> PlantProtectants);
}
