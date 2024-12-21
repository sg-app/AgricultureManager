using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Features.TaxRateFeatures
{
    public record GetTaxRateListCommand : IReq<IEnumerable<TaxRateVm>> { }

    public class GetTaxRateListCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetTaxRateListCommand, IEnumerable<TaxRateVm>>
    {
        public async Task<Response<IEnumerable<TaxRateVm>>> Handle(GetTaxRateListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.TaxRate.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IEnumerable<TaxRateVm>>(entities));
        }
    }
}
