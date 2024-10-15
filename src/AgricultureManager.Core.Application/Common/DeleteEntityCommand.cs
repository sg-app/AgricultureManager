using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Domain.Entities;
using MediatR;
using System.Reflection;

namespace AgricultureManager.Core.Application.Common
{
    public class DeleteEntityCommand<TViewModel> : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteEntityCommandHandler<TViewModel>(IAppDbContext dbContext) : IRequestHandler<DeleteEntityCommand<TViewModel>>
    {
        public async Task Handle(DeleteEntityCommand<TViewModel> request, CancellationToken cancellationToken)
        {
            var viewModelName = typeof(TViewModel).Name;

            if (!viewModelName.EndsWith("Vm"))
                return;
            //return Response.Fail($"Typ muss mit Vm enden. Typ aktuell: {viewModelName}");

            var entityName = viewModelName.Substring(0, viewModelName.Length - "Vm".Length);

            var entityType = Assembly.GetAssembly(typeof(Parameter))?.GetTypes().FirstOrDefault(t => t.Name == entityName);

            if (entityType is null)
                return;
            //return Response.Fail($"Entity Typ mit Name {entityName} wurde nicht gefunden.");

            var entity = await dbContext.FindAsync(entityType, [request.Id], cancellationToken);

            if (entity is null)
                return;
            //return Response.Fail($"Entity von Typ {entityType} mit der ID {request.Id} wurde nicht gefunden.");

            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return Response.Success("Entity wurde erfolgreich gelöscht.");
        }
    }
}
