using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.SeedCategoryFeatures
{
    public class UpdateSeedCategoryCommand : IReq<SeedCategoryVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }

    public class UpdateSeedCategoryCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateSeedCategoryCommand, SeedCategoryVm>
    {
        public async Task<Response<SeedCategoryVm>> Handle(UpdateSeedCategoryCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var seedCategory = await dbContext.SeedCategory.FindAsync([request.Id], cancellationToken);
            if (seedCategory is null)
            {
                return Response.Fail<SeedCategoryVm>("Saatgutkategorie nicht gefunden.");
            }

            mapper.Map(request, seedCategory);

            dbContext.SeedCategory.Update(seedCategory);
            await dbContext.SaveChangesAsync(cancellationToken);

            var seedCategoryVm = mapper.Map<SeedCategoryVm>(seedCategory);
            return Response.Success(seedCategoryVm);
        }
    }
}
