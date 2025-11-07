

namespace AgricultureManager.Core.Application.Shared.Interfaces
{
    public interface IMasterDataLoader
    {
        Type ViewModelType { get; }
        Task<object> LoadDataAsync();
    }
}
