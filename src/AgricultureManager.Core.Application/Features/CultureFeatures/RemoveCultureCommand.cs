using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.CultureFeatures
{
    public class RemoveCultureCommand : IReq
    {
        public Guid Id { get; set; }
    }

    public class RemoveCultureCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<RemoveCultureCommand>
    {
        public async Task<ResponseLess> Handle(RemoveCultureCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Culture.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Kultur nicht gefunden.");

            dbContext.Culture.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
