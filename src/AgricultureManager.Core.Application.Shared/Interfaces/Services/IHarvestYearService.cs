using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Shared.Interfaces.Services
{
    public interface IHarvestYearService
    {
        event EventHandler<HarvestYearVm> HarvestYearChanged;

        ValueTask<HarvestYearVm?> GetSelectedYearAsync();
        ValueTask SetSelectedYearAsync(HarvestYearVm harvestYear);
    }
}