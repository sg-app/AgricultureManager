using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.SeedTechnologyFeatures
{
    public class AddSeedTechnologyCommand : IReq<SeedTechnologyVm>
    {
        public string Number { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
    public class AddSeedTechnologyCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddSeedTechnologyCommand, SeedTechnologyVm>
    {
        public async Task<Response<SeedTechnologyVm>> Handle(AddSeedTechnologyCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var seedTechnology = mapper.Map<SeedTechnology>(request);
            seedTechnology.Id = Guid.NewGuid();

            await dbContext.SeedTechnology.AddAsync(seedTechnology, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var seedTechnologyVm = mapper.Map<SeedTechnologyVm>(seedTechnology);
            return Response.Success(seedTechnologyVm);
        }
    }
}
