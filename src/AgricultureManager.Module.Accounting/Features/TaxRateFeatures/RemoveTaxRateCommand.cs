using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Persistence;

namespace AgricultureManager.Module.Accounting.Features.TaxRateFeatures
{
    public record RemoveTaxRateCommand(Guid Id) : IReq { }

    public class RemoveTaxRateCommandHandler(IAccountingDbContextFactory dbContextFactory) : IReqHandler<RemoveTaxRateCommand>
    {
        public async Task<ResponseLess> Handle(RemoveTaxRateCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var entity = await dbContext.TaxRate.FindAsync([request.Id], cancellationToken);
            if (entity is null)
                return Response.Fail("Steuerrate nicht gefunden.");

            dbContext.TaxRate.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Response.Success();
        }
    }
}
