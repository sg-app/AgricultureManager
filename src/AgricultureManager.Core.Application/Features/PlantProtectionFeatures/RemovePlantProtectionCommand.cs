using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.PlantProtectionFeatures
{
    public record RemovePlantProtectionCommand(Guid Id) : IReq { }

    public class RemovePlantProtectionCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemovePlantProtectionCommand>
    {
        public async Task<ResponseLess> Handle(RemovePlantProtectionCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.PlantProtection.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Pflanzenschutz nicht gefunden.");

            dbContext.PlantProtection.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
