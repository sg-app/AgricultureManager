using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.PlantProtectantFeatures
{
    public class RemovePlantProtectantCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemovePlantProtectantCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemovePlantProtectantCommand>
    {
        public async Task<ResponseLess> Handle(RemovePlantProtectantCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.PlantProtectant.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Pflanzenschutzmittel nicht gefunden.");

            dbContext.PlantProtectant.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
