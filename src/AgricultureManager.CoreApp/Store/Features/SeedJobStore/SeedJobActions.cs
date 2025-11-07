using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.CoreApp.Store.Features.SeedJobStore
{
    public record LoadSeedJobDataAction(Guid HarvestUnitId) : ILoadDataAction { }
    public record LoadSeedJobDataResultAction(IEnumerable<SeedVm> Seeds);
    public record LoadSeedJobDataResultFailAction();
    public record AddSeedJobAction(SeedVm Seed);
    public record UpdateSeedJobAction(SeedVm Seed);
    public record RemoveSeedJobAction(Guid SeedId);

}
