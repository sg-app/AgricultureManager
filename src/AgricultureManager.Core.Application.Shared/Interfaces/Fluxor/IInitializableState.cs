namespace AgricultureManager.Core.Application.Shared.Interfaces.Fluxor
{
    public interface IInitializableState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
    }
}
