using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.SeedCategoryFeatures
{
    public class RemoveSeedCategoryCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveSeedCategoryCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveSeedCategoryCommand>
    {
        public async Task<ResponseLess> Handle(RemoveSeedCategoryCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.SeedCategory.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Saatgutkategorie nicht gefunden.");

            dbContext.SeedCategory.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
