

namespace AgricultureManager.Core.Application.Shared.Interfaces
{
    public interface IMasterDataLoader<TViewModel>
    {
        Task<List<TViewModel>> LoadDataAsync();
    }
}
