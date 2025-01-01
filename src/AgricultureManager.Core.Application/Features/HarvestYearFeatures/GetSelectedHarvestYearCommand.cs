using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Keys;
using AgricultureManager.Core.Application.Shared.Models;
using MediatR;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Features.HarvestYearFeatures
{
    public record GetSelectedHarvestYearCommand : IReq<HarvestYearVm>
    {
    }
    public class GetSelectedHarvestYearCommandHandler(IMediator mediator) : IReqHandler<GetSelectedHarvestYearCommand, HarvestYearVm>
    {
        public async Task<Response<HarvestYearVm>> Handle(GetSelectedHarvestYearCommand request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetParameterCommand { Key = ParameterKeys.SelectedHarvestYear }, cancellationToken);
            if (response.Data is null)
                return Response.Fail<HarvestYearVm>("Keine Daten in Datenbank erhalten.");

            var harvestYear = JsonSerializer.Deserialize<HarvestYearVm>(response.Data);
            if (harvestYear is null)
                return Response.Fail<HarvestYearVm>("Fehler beim Laden des ausgewählten Erntejahres. Die Wiederherstellung der Daten aus der Datenbank schlug fehl!");

            return Response.Success(harvestYear);
        }
    }
}
