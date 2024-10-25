using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FieldFeatures
{
    public class RemoveFieldCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveFieldCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFieldCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFieldCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Field.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Feld nicht gefunden.");

            dbContext.Field.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
