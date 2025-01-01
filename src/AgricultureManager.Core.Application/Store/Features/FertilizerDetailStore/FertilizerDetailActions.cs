using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.FertilizerDetailStore
{
    public record LoadFertilizerDetailsDataAction();
    public record LoadFertilizerDetailsDataResultAction(IEnumerable<FertilizerDetailVm> FertilizerDetails);
    public record LoadFertilizerDetailDataResultFailAction();
    public record AddFertilizerDetailAction(FertilizerDetailVm FertilizerDetail);
    public record UpdateFertilizerDetailAction(FertilizerDetailVm FertilizerDetail);
    public record RemoveFertilizerDetailAction(Guid FertilizerDetailId);
}
