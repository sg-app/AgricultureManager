using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerFeatures
{
    public class AddFertilizerCommand : IReq<FertilizerVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Detail { get; set; }
        public string? Comment { get; set; }
    }
    public class AddFertilizerCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFertilizerCommand, FertilizerVm>
    {
        public async Task<Response<FertilizerVm>> Handle(AddFertilizerCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var fertilizer = mapper.Map<Fertilizer>(request);
            fertilizer.Id = Guid.NewGuid();

            await dbContext.Fertilizer.AddAsync(fertilizer, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fertilizerVm = mapper.Map<FertilizerVm>(fertilizer);
            return Response.Success(fertilizerVm);
        }
    }
}
