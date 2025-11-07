using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.CoreApp.Store.Features.HarvestYearStore
{
    public record GetCurrentHarvestYearAction();
    public record SetSelectedHarvestYearAction(HarvestYearVm SelectedHarvestYear);
    public record SaveSelectedHarvestYearAction(HarvestYearVm SelectedHarvestYear);
}
