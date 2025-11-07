using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.CoreApp.Store.Features.HarvestUnitStore
{
    public record LoadHarvestUnitsDataAction(HarvestYearVm? HarvestYear);
    public record LoadHarvestUnitsDataResultAction(IEnumerable<HarvestUnitOverview> HarvestUnitsOverview);
    public record SetSelectedHarvestUnitsAction(IList<HarvestUnitOverview> HarvestUnitsOverview);
}
