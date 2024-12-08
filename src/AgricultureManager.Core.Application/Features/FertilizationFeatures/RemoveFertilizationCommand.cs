using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FertilizationFeatures
{
    public record RemoveFertilizationCommand(Guid Id) : IReq { }

    public class RemoveFertilizationCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFertilizationCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFertilizationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Fertilization.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Düngung nicht gefunden.");

            dbContext.Fertilization.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
