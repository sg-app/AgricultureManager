using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.HarvestJobStore
{
    public record LoadHarvestJobDataAction(Guid HarvestUnitId) : ILoadDataAction { }
    public record LoadHarvestJobDataResultAction(IEnumerable<HarvestVm> Harvests);
    public record LoadHarvestJobDataResultFailAction();
    public record AddHarvestJobAction(HarvestVm Harvest);
    public record UpdateHarvestJobAction(HarvestVm Harvest);
    public record RemoveHarvestJobAction(Guid HarvestId);

}
