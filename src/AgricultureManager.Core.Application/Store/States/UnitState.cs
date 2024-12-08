using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record UnitState
    {
        public bool IsLoading { get; init; }
        public IEnumerable<UnitVm>? Units { get; init; }
        private UnitState() { }

        public UnitState(bool isLoading, IEnumerable<UnitVm>? units)
        {
            IsLoading = isLoading;
            Units = units;
        }
    }
}
