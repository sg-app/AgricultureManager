using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.SeedTechnologyFeatures
{
    public class RemoveSeedTechnologyCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveSeedTechnologyCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveSeedTechnologyCommand>
    {
        public async Task<ResponseLess> Handle(RemoveSeedTechnologyCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.SeedTechnology.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Saattechnologie nicht gefunden.");

            dbContext.SeedTechnology.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
