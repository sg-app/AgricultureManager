using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.SeedCategoryFeatures
{
    public class AddSeedCategoryCommand : IReq<SeedCategoryVm>
    {
        public string Number { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
    public class AddSeedCategoryCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddSeedCategoryCommand, SeedCategoryVm>
    {
        public async Task<Response<SeedCategoryVm>> Handle(AddSeedCategoryCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var seedCategory = mapper.Map<SeedCategory>(request);
            seedCategory.Id = Guid.NewGuid();

            await dbContext.SeedCategory.AddAsync(seedCategory, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var seedCategoryVm = mapper.Map<SeedCategoryVm>(seedCategory);
            return Response.Success(seedCategoryVm);
        }
    }
}
