using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.SeedTechnologyFeatures
{
    public class UpdateSeedTechnologyCommand : IReq<SeedTechnologyVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }

    public class UpdateSeedTechnologyCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateSeedTechnologyCommand, SeedTechnologyVm>
    {
        public async Task<Response<SeedTechnologyVm>> Handle(UpdateSeedTechnologyCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var seedTechnology = await dbContext.SeedTechnology.FindAsync([request.Id], cancellationToken);
            if (seedTechnology is null)
            {
                return Response.Fail<SeedTechnologyVm>("Saattechnologie nicht gefunden.");
            }

            mapper.Map(request, seedTechnology);

            dbContext.SeedTechnology.Update(seedTechnology);
            await dbContext.SaveChangesAsync(cancellationToken);

            var seedTechnologyVm = mapper.Map<SeedTechnologyVm>(seedTechnology);
            return Response.Success(seedTechnologyVm);
        }
    }
}
