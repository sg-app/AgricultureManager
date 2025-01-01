using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.CultureStore
{
    public record LoadCulturesDataAction();
    public record LoadCulturesDataResultAction(IEnumerable<CultureVm> Cultures);
    public record LoadCultureDataResultFailAction();
    public record AddCultureAction(CultureVm Culture);
    public record UpdateCultureAction(CultureVm Culture);
    public record RemoveCultureAction(Guid CultureId);
}
