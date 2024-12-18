using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningSpecificationFeatures
{
    public record GetFertilizerPlaningSpecificationCommand(Guid HarvestUnitId) : IReq<ICollection<FertilizerPlaningSpecificationVm>>
    {
    }

    public class GetFertilizerPlaningSpecificationCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<GetFertilizerPlaningSpecificationCommand, ICollection<FertilizerPlaningSpecificationVm>>
    {
        public async Task<Response<ICollection<FertilizerPlaningSpecificationVm>>> Handle(GetFertilizerPlaningSpecificationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var entities = await dbContext.FertilizerPlaningSpecification
                .AsNoTracking()
                .Where(x => x.HarvestUnitId == request.HarvestUnitId)
                .Include(f => f.FertilizerDetail)
                .ToListAsync(cancellationToken);

            return Response.Success(mapper.Map<ICollection<FertilizerPlaningSpecificationVm>>(entities));
        }
    }
}
