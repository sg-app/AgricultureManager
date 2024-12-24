using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Module.Accounting.Features.AccountFeatures
{
    public record GetAccountListCommand : IReq<IEnumerable<AccountVm>> { }

    public class GetAccountListCommandHandler(IAccountingDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetAccountListCommand, IEnumerable<AccountVm>>
    {
        public async Task<Response<IEnumerable<AccountVm>>> Handle(GetAccountListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.Account.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IEnumerable<AccountVm>>(entities));
        }
    }
}
