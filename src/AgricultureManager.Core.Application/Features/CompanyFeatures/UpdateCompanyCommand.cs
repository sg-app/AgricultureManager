﻿using AgricultureManager.Core.Application.Features.ParameterFeatures;
using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Keys;
using AgricultureManager.Core.Application.Shared.Models;
using MediatR;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Features.CompanyFeatures
{
    public class UpdateCompanyCommand : IReq<CompanyVm>
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
    public class UpdateCompanyCommandHandler(IMediator mediator) : IReqHandler<UpdateCompanyCommand, CompanyVm>
    {
        public async Task<Response<CompanyVm>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {

            var keyValue = new UpdateParameterCommand
            {
                Key = ParameterKeys.Company,
                Value = JsonSerializer.Serialize(request)
            };

            var response = await mediator.Send(keyValue, cancellationToken);
            if (response.Data is null)
                return Response.Fail<CompanyVm>("Keine Daten von Datenbank erhalten.");

            var company = JsonSerializer.Deserialize<CompanyVm>(response.Data.Value);
            if (company is null)
                return Response.Fail<CompanyVm>("Fehler beim Aktualisieren des Unternehmens. Das lesen der Daten aus der Datenbank schlug fehl!");

            return Response.Success(company);
        }
    }


}
