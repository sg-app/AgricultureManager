using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.FertilizerFeatures
{
    public class RemoveFertilizerCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveFertilizerCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveFertilizerCommand>
    {
        public async Task<ResponseLess> Handle(RemoveFertilizerCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Fertilizer.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Dünger nicht gefunden.");

            dbContext.Fertilizer.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
