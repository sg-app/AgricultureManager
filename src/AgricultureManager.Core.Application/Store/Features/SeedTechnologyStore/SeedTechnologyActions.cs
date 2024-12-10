using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.SeedTechnologyStore
{
    public record LoadSeedTechnologiesDataAction();
    public record LoadSeedTechnologiesDataResultAction(IEnumerable<SeedTechnologyVm> SeedTechnologies);
    public record LoadSeedTechnologyDataResultFailAction();
    public record AddSeedTechnologyAction(SeedTechnologyVm SeedTechnology);
    public record UpdateSeedTechnologyAction(SeedTechnologyVm SeedTechnology);
    public record RemoveSeedTechnologyAction(Guid SeedTechnologyId);
}
