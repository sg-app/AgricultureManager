using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.SeedTechnologyStore
{
    public record LoadSeedTechnologiesDataAction();
    public record LoadSeedTechnologiesDataResultAction(IEnumerable<SeedTechnologyVm> SeedTechnologies);
}
