using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.UnitStore
{
    public record LoadUnitsDataAction();
    public record LoadUnitsDataResultAction(IEnumerable<UnitVm> Units);
    public record LoadUnitDataResultFailAction();
    public record AddUnitAction(UnitVm Unit);
    public record UpdateUnitAction(UnitVm Unit);
    public record RemoveUnitAction(Guid UnitId);
}
