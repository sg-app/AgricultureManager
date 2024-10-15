using AgricultureManager.Core.Application.Common;
using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using MediatR;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Features.CompanyFeatures
{
    public class AddCompanyCommand : IReq<CompanyVm>
    {
        public string? CompanyName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? Housenumber { get; set; }
        public string? Plz { get; set; }
        public string? City { get; set; }
        public string CompanyNumber { get; set; } = string.Empty;
    }
    public class AddCompanyCommandHandler(IMediator mediator) : IReqHandler<AddCompanyCommand, CompanyVm>
    {
        public async Task<Response<CompanyVm>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var keyValue = new AddParameterCommand
            {
                Key = ParameterKeys.Company,
                Value = JsonSerializer.Serialize(request)
            };

            var response = await mediator.Send(keyValue, cancellationToken);

            if (response?.Data is null)
                return Response.Fail<CompanyVm>("Keine Daten von Datenbank erhalten.");

            var company = JsonSerializer.Deserialize<CompanyVm>(response.Data.Value);
            if (company is null)
                return Response.Fail<CompanyVm>("Fehler beim Speichern des Unternehmens.");

            return Response.Success(company);
        }
    }
}
