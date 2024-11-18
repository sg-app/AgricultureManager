using AgricultureManager.Core.Application.Common;
using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using MediatR;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Features.HarvestYearFeatures
{
    public record SetSelectedHarvestYearCommand(HarvestYearVm HarvestYear) : IReq<HarvestYearVm> { }

    public class SetSelectedHarvestYearHandler(IMediator mediator) : IReqHandler<SetSelectedHarvestYearCommand, HarvestYearVm>
    {
        public async Task<Response<HarvestYearVm>> Handle(SetSelectedHarvestYearCommand request, CancellationToken cancellationToken)
        {
            var keyValue = new AddParameterCommand
            {
                Key = ParameterKeys.SelectedHarvestYear,
                Value = JsonSerializer.Serialize(request.HarvestYear)
            };

            var response = await mediator.Send(keyValue, cancellationToken);

            if (response?.Data is null)
                return Response.Fail<HarvestYearVm>("Keine Daten von Datenbank erhalten.");

            var harvestYear = JsonSerializer.Deserialize<HarvestYearVm>(response.Data.Value);
            if (harvestYear is null)
                return Response.Fail<HarvestYearVm>("Fehler beim Speichern des ausgewählten Erntejahr.");

            return Response.Success(harvestYear);
        }
    }
}
