using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.SeedCategoryStore
{
    public record LoadSeedCategoriesDataAction();
    public record LoadSeedCategoriesDataResultAction(IEnumerable<SeedCategoryVm> SeedCategories);
}
