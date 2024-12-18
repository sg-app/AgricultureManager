using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FertilizerPlaningSpecificationFeatures
{
    public class AddFertilizerPlaningSpecificationCommand : IReq<FertilizerPlaningSpecificationVm>
    {
        public Guid HarvestUnitId { get; set; }
        public Guid FertilizerDetailId { get; set; }
        public int Quantity { get; set; }
    }
    public class AddFertilizerPlaningSpecificationCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFertilizerPlaningSpecificationCommand, FertilizerPlaningSpecificationVm>
    {
        public async Task<Response<FertilizerPlaningSpecificationVm>> Handle(AddFertilizerPlaningSpecificationCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var field = mapper.Map<FertilizerPlaningSpecification>(request);
            field.Id = Guid.NewGuid();

            await dbContext.FertilizerPlaningSpecification.AddAsync(field, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<FertilizerPlaningSpecificationVm>(field);
            return Response.Success(fieldVm);
        }
    }
}
