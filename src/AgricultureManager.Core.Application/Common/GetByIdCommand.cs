using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Reflection;

namespace AgricultureManager.Core.Application.Common
{
    public class GetByIdCommand<TViewModel> : IReq<TViewModel>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdCommandHandler<TViewModel>(IAppDbContext dbContext, IMapper mapper) : IReqHandler<GetByIdCommand<TViewModel>, TViewModel>
    {
        public async Task<Response<TViewModel>> Handle(GetByIdCommand<TViewModel> request, CancellationToken cancellationToken)
        {
            var viewModelName = typeof(TViewModel).Name;

            if (!viewModelName.EndsWith("Vm"))
                return Response.Fail<TViewModel>($"Typ muss mit Vm enden. Typ aktuell: {viewModelName}");

            var entityName = viewModelName.Substring(0, viewModelName.Length - "Vm".Length);

            var entityType = (Assembly.GetAssembly(typeof(Parameter))?.GetTypes().FirstOrDefault(t => t.Name == entityName));
            if (entityType is null)
                return Response.Fail<TViewModel>($"Entity Typ mit Name {entityName} wurde nicht gefunden.");


            var entity = await dbContext.FindAsync(entityType, [request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail<TViewModel>($"Entity von Typ {entityType} mit der ID {request.Id} wurde nicht gefunden.");


            return Response.Success(mapper.Map<TViewModel>(entity));
        }
    }
}
