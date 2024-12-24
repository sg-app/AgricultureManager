using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;

namespace AgricultureManager.Module.Accounting.Features.AccountFeatures
{
    public record RemoveAccountCommand(Guid Id) : IReq { }

    public class RemoveAccountCommandHandler(IAccountingDbContextFactory dbContextFactory) : IReqHandler<RemoveAccountCommand>
    {
        public async Task<ResponseLess> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.Account.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Konto nicht gefunden.");

            dbContext.Account.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
