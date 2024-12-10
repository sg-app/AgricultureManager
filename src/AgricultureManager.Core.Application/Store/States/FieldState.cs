using AgricultureManager.Core.Application.Shared.Interfaces.Fluxor;
using AgricultureManager.Core.Application.Shared.Models;
using Fluxor;

namespace AgricultureManager.Core.Application.Store.States
{
    [FeatureState]
    public record FieldState : IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public IEnumerable<FieldVm> Fields { get; init; } = [];
        private FieldState() { }

        public FieldState(bool isLoading, IEnumerable<FieldVm> fields)
        {
            IsLoading = isLoading;
            Fields = fields;
        }
    }
}
