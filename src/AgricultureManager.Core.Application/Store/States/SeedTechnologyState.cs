using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record SeedTechnologyState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<SeedTechnologyVm> SeedTechnologies { get; init; } = [];

        private SeedTechnologyState() { }

        public SeedTechnologyState(bool isLoading, IEnumerable<SeedTechnologyVm> seedTechnologies)
        {
            IsLoading = isLoading;
            SeedTechnologies = seedTechnologies;
        }
    }
}
