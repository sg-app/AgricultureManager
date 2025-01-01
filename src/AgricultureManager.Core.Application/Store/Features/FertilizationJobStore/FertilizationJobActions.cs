using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.FertilizationJobStore
{
    public record LoadFertilizationJobDataAction(Guid HarvestUnitId) : ILoadDataAction { }
    public record LoadFertilizationJobDataResultAction(IEnumerable<FertilizationVm> Fertilizations);
    public record LoadFertilizationJobDataResultFailAction();
    public record AddFertilizationJobAction(FertilizationVm Fertilization);
    public record UpdateFertilizationJobAction(FertilizationVm Fertilization);
    public record RemoveFertilizationJobAction(Guid FertilizationId);

}
