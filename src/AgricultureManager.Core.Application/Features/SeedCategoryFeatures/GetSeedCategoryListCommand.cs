using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.SeedCategoryFeatures
{
    public class GetSeedCategoryListCommand : IReq<IList<SeedCategoryVm>>
    {
    }

    public class GetSeedCategoryListCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetSeedCategoryListCommand, IList<SeedCategoryVm>>
    {
        public async Task<Response<IList<SeedCategoryVm>>> Handle(GetSeedCategoryListCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.SeedCategory.AsNoTracking().ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<IList<SeedCategoryVm>>(entities));
        }
    }
}
