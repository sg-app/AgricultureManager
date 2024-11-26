using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.HarvestYearStore
{
    public record LoadHarvestYearsAction();
    public record LoadHarvestYearsResultAction(IEnumerable<HarvestYearVm>? HarvestYears);
    public record GetCurrentHarvestYearAction();
    public record SetSelectedHarvestYearAction(HarvestYearVm SelectedHarvestYear);
    public record SaveSelectedHarvestYearAction(HarvestYearVm SelectedHarvestYear);
}
