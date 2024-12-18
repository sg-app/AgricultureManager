using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningSpecificationFeatures
{
    public record RemoveFertilizerPlaningSpecificationCommand(Guid Id) : IReq
    {
    }

    public class RemoveFertilizerPlaningSpecificationCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFertilizerPlaningSpecificationCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFertilizerPlaningSpecificationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.FertilizerPlaningSpecification.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Eintrag nicht gefunden.");

            dbContext.FertilizerPlaningSpecification.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
