using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerStore
{
    public record LoadFertilizersDataAction();
    public record LoadFertilizersDataResultAction(IEnumerable<FertilizerVm> Fertilizers);
}
