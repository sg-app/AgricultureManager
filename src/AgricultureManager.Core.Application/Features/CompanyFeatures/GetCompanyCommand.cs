using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Keys;
using AgricultureManager.Core.Application.Shared.Models;
using MediatR;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Features.CompanyFeatures
{
    public class GetCompanyCommand : IReq<CompanyVm>
    {
    }
    public class GetCompanyCommandHandler(IMediator mediator) : IReqHandler<GetCompanyCommand, CompanyVm>
    {
        public async Task<Response<CompanyVm>> Handle(GetCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetParameterCommand { Key = ParameterKeys.Company }, cancellationToken);
            if (response.Data is null)
                return Response.Fail<CompanyVm>("Keine Daten von Datenbank erhalten.");

            var company = JsonSerializer.Deserialize<CompanyVm>(response.Data);
            if (company is null)
                return Response.Fail<CompanyVm>("Fehler beim Speichern des Unternehmens. Die Wiederherstellung der Daten aus der Datenbank schlug fehl!");

            return Response.Success(company);
        }
    }
}
