using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningFeatures
{
    public record RemoveFertilizerPlaningCommand(Guid Id) : IReq
    {
    }

    public class RemoveFertilizerPlaningCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFertilizerPlaningCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFertilizerPlaningCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.FertilizerPlaning.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Eintrag nicht gefunden.");

            dbContext.FertilizerPlaning.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
