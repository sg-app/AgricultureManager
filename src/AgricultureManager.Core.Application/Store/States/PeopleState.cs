using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record PeopleState
    {
        public bool IsLoading { get; init; }
        public IEnumerable<PersonVm>? Peoples { get; init; }
        private PeopleState() { }

        public PeopleState(bool isLoading, IEnumerable<PersonVm>? peoples)
        {
            IsLoading = isLoading;
            Peoples = peoples;
        }
    }
}
