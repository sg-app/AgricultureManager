using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.UnitFeatures
{
    public class RemoveUnitCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveUnitCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveUnitCommand>
    {
        public async Task<ResponseLess> Handle(RemoveUnitCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Unit.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Einheit nicht gefunden.");

            dbContext.Unit.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
