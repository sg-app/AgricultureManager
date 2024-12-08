using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.SeedFeatures
{
    public record RemoveSeedCommand(Guid Id) : IReq { }

    public class RemoveSeedCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveSeedCommand>
    {
        public async Task<ResponseLess> Handle(RemoveSeedCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Seed.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Aussaat nicht gefunden.");

            dbContext.Seed.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
