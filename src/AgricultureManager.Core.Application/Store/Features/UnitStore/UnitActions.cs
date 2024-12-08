using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.UnitStore
{
    public record LoadUnitsDataAction();
    public record LoadUnitsDataResultAction(IEnumerable<UnitVm> Units);
}
