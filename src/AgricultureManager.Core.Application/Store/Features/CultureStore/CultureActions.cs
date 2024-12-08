using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.CultureStore
{
    public record LoadCulturesDataAction();
    public record LoadCulturesDataResultAction(IEnumerable<CultureVm> Cultures);
}
