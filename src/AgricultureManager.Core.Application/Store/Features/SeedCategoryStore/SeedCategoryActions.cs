using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.SeedCategoryStore
{
    public record LoadSeedCategoriesDataAction();
    public record LoadSeedCategoriesDataResultAction(IEnumerable<SeedCategoryVm> SeedCategories);
    public record LoadSeedCategoryDataResultFailAction();
    public record AddSeedCategoryAction(SeedCategoryVm SeedCategory);
    public record UpdateSeedCategoryAction(SeedCategoryVm SeedCategory);
    public record RemoveSeedCategoryAction(Guid SeedCategoryId);
}
