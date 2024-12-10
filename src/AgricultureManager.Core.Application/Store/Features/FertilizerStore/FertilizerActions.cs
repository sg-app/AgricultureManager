using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerStore
{
    public record LoadFertilizersDataAction();
    public record LoadFertilizersDataResultAction(IEnumerable<FertilizerVm> Fertilizers);
    public record LoadFertilizerDataResultFailAction();
    public record AddFertilizerAction(FertilizerVm Fertilizer);
    public record UpdateFertilizerAction(FertilizerVm Fertilizer);
    public record RemoveFertilizerAction(Guid FertilizerId);
}
