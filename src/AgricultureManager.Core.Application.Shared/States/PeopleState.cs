using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Shared.States
{
    [FeatureState]
    public record PeopleState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<PersonVm> Peoples { get; init; } = [];
        private PeopleState() { }

        public PeopleState(bool isLoading, IEnumerable<PersonVm> peoples)
        {
            IsLoading = isLoading;
            Peoples = peoples;
        }
    }
}
